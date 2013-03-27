#region License

// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

#endregion

#if !(SILVERLIGHT || PORTABLE || NETFX_CORE)
using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
#if NET20
using NetDimension.Json.Utilities.LinqBridge;
#endif

namespace NetDimension.Json.Utilities
{
    internal class DynamicReflectionDelegateFactory : ReflectionDelegateFactory
    {
        public static DynamicReflectionDelegateFactory Instance = new DynamicReflectionDelegateFactory();

        private static DynamicMethod CreateDynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
        {
            var dynamicMethod = !owner.IsInterface()
                                    ? new DynamicMethod(name, returnType, parameterTypes, owner, true)
                                    : new DynamicMethod(name, returnType, parameterTypes, owner.Module, true);

            return dynamicMethod;
        }

        public override MethodCall<T, object> CreateMethodCall<T>(MethodBase method)
        {
            var dynamicMethod = CreateDynamicMethod(method.ToString(), typeof (object),
                                                    new[] {typeof (object), typeof (object[])}, method.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            GenerateCreateMethodCallIL(method, generator);

            return (MethodCall<T, object>) dynamicMethod.CreateDelegate(typeof (MethodCall<T, object>));
        }

        private void GenerateCreateMethodCallIL(MethodBase method, ILGenerator generator)
        {
            var args = method.GetParameters();

            var argsOk = generator.DefineLabel();

            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Ldlen);
            generator.Emit(OpCodes.Ldc_I4, args.Length);
            generator.Emit(OpCodes.Beq, argsOk);

            generator.Emit(OpCodes.Newobj,
                           typeof (TargetParameterCountException).GetConstructor(ReflectionUtils.EmptyTypes));
            generator.Emit(OpCodes.Throw);

            generator.MarkLabel(argsOk);

            if (!method.IsConstructor && !method.IsStatic)
                generator.PushInstance(method.DeclaringType);

            for (var i = 0; i < args.Length; i++)
            {
                generator.Emit(OpCodes.Ldarg_1);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Ldelem_Ref);

                generator.UnboxIfNeeded(args[i].ParameterType);
            }

            if (method.IsConstructor)
                generator.Emit(OpCodes.Newobj, (ConstructorInfo) method);
            else if (method.IsFinal || !method.IsVirtual)
                generator.CallMethod((MethodInfo) method);

            var returnType = method.IsConstructor
                                 ? method.DeclaringType
                                 : ((MethodInfo) method).ReturnType;

            if (returnType != typeof (void))
                generator.BoxIfNeeded(returnType);
            else
                generator.Emit(OpCodes.Ldnull);

            generator.Return();
        }

        public override Func<T> CreateDefaultConstructor<T>(Type type)
        {
            var dynamicMethod = CreateDynamicMethod("Create" + type.FullName, typeof (T), ReflectionUtils.EmptyTypes,
                                                    type);
            dynamicMethod.InitLocals = true;
            var generator = dynamicMethod.GetILGenerator();

            GenerateCreateDefaultConstructorIL(type, generator);

            return (Func<T>) dynamicMethod.CreateDelegate(typeof (Func<T>));
        }

        private void GenerateCreateDefaultConstructorIL(Type type, ILGenerator generator)
        {
            if (type.IsValueType())
            {
                generator.DeclareLocal(type);
                generator.Emit(OpCodes.Ldloc_0);
                generator.Emit(OpCodes.Box, type);
            }
            else
            {
                var constructorInfo =
                    type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null,
                                        ReflectionUtils.EmptyTypes, null);

                if (constructorInfo == null)
                    throw new ArgumentException(
                        "Could not get constructor for {0}.".FormatWith(CultureInfo.InvariantCulture, type));

                generator.Emit(OpCodes.Newobj, constructorInfo);
            }

            generator.Return();
        }

        public override Func<T, object> CreateGet<T>(PropertyInfo propertyInfo)
        {
            var dynamicMethod = CreateDynamicMethod("Get" + propertyInfo.Name, typeof (T), new[] {typeof (object)},
                                                    propertyInfo.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            GenerateCreateGetPropertyIL(propertyInfo, generator);

            return (Func<T, object>) dynamicMethod.CreateDelegate(typeof (Func<T, object>));
        }

        private void GenerateCreateGetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
        {
            var getMethod = propertyInfo.GetGetMethod(true);
            if (getMethod == null)
                throw new ArgumentException(
                    "Property '{0}' does not have a getter.".FormatWith(CultureInfo.InvariantCulture, propertyInfo.Name));

            if (!getMethod.IsStatic)
                generator.PushInstance(propertyInfo.DeclaringType);

            generator.CallMethod(getMethod);
            generator.BoxIfNeeded(propertyInfo.PropertyType);
            generator.Return();
        }

        public override Func<T, object> CreateGet<T>(FieldInfo fieldInfo)
        {
            var dynamicMethod = CreateDynamicMethod("Get" + fieldInfo.Name, typeof (T), new[] {typeof (object)},
                                                    fieldInfo.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            GenerateCreateGetFieldIL(fieldInfo, generator);

            return (Func<T, object>) dynamicMethod.CreateDelegate(typeof (Func<T, object>));
        }

        private void GenerateCreateGetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
        {
            if (!fieldInfo.IsStatic)
                generator.PushInstance(fieldInfo.DeclaringType);

            generator.Emit(OpCodes.Ldfld, fieldInfo);
            generator.BoxIfNeeded(fieldInfo.FieldType);
            generator.Return();
        }

        public override Action<T, object> CreateSet<T>(FieldInfo fieldInfo)
        {
            var dynamicMethod = CreateDynamicMethod("Set" + fieldInfo.Name, null, new[] {typeof (T), typeof (object)},
                                                    fieldInfo.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            GenerateCreateSetFieldIL(fieldInfo, generator);

            return (Action<T, object>) dynamicMethod.CreateDelegate(typeof (Action<T, object>));
        }

        internal static void GenerateCreateSetFieldIL(FieldInfo fieldInfo, ILGenerator generator)
        {
            if (!fieldInfo.IsStatic)
                generator.PushInstance(fieldInfo.DeclaringType);

            generator.Emit(OpCodes.Ldarg_1);
            generator.UnboxIfNeeded(fieldInfo.FieldType);
            generator.Emit(OpCodes.Stfld, fieldInfo);
            generator.Return();
        }

        public override Action<T, object> CreateSet<T>(PropertyInfo propertyInfo)
        {
            var dynamicMethod = CreateDynamicMethod("Set" + propertyInfo.Name, null, new[] {typeof (T), typeof (object)},
                                                    propertyInfo.DeclaringType);
            var generator = dynamicMethod.GetILGenerator();

            GenerateCreateSetPropertyIL(propertyInfo, generator);

            return (Action<T, object>) dynamicMethod.CreateDelegate(typeof (Action<T, object>));
        }

        internal static void GenerateCreateSetPropertyIL(PropertyInfo propertyInfo, ILGenerator generator)
        {
            var setMethod = propertyInfo.GetSetMethod(true);
            if (!setMethod.IsStatic)
                generator.PushInstance(propertyInfo.DeclaringType);

            generator.Emit(OpCodes.Ldarg_1);
            generator.UnboxIfNeeded(propertyInfo.PropertyType);
            generator.CallMethod(setMethod);
            generator.Return();
        }
    }
}

#endif