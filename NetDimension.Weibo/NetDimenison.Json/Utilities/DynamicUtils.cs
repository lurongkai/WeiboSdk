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

#if !(WINDOWS_PHONE || PORTABLE)
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using NetDimension.Json.Serialization;

namespace NetDimension.Json.Utilities
{
    internal static class DynamicUtils
    {
        public static bool TryGetMember(this IDynamicMetaObjectProvider dynamicProvider, string name, out object value)
        {
            ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");

            var getMemberBinder = (GetMemberBinder) BinderWrapper.GetMember(name, typeof (DynamicUtils));

            var callSite = CallSite<Func<CallSite, object, object>>.Create(new NoThrowGetBinderMember(getMemberBinder));

            var result = callSite.Target(callSite, dynamicProvider);

            if (!ReferenceEquals(result, NoThrowExpressionVisitor.ErrorResult))
            {
                value = result;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        public static bool TrySetMember(this IDynamicMetaObjectProvider dynamicProvider, string name, object value)
        {
            ValidationUtils.ArgumentNotNull(dynamicProvider, "dynamicProvider");

            var binder = (SetMemberBinder) BinderWrapper.SetMember(name, typeof (DynamicUtils));

            var setterSite = CallSite<Func<CallSite, object, object, object>>.Create(new NoThrowSetBinderMember(binder));

            var result = setterSite.Target(setterSite, dynamicProvider, value);

            return !ReferenceEquals(result, NoThrowExpressionVisitor.ErrorResult);
        }

        public static IEnumerable<string> GetDynamicMemberNames(this IDynamicMetaObjectProvider dynamicProvider)
        {
            var metaObject = dynamicProvider.GetMetaObject(Expression.Constant(dynamicProvider));
            return metaObject.GetDynamicMemberNames();
        }

        internal static class BinderWrapper
        {
#if !SILVERLIGHT
            public const string CSharpAssemblyName =
                "Microsoft.CSharp, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
#else
      public const string CSharpAssemblyName = "Microsoft.CSharp, Version=2.0.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35";
#endif

            private const string BinderTypeName = "Microsoft.CSharp.RuntimeBinder.Binder, " + CSharpAssemblyName;

            private const string CSharpArgumentInfoTypeName =
                "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfo, " + CSharpAssemblyName;

            private const string CSharpArgumentInfoFlagsTypeName =
                "Microsoft.CSharp.RuntimeBinder.CSharpArgumentInfoFlags, " + CSharpAssemblyName;

            private const string CSharpBinderFlagsTypeName =
                "Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags, " + CSharpAssemblyName;

            private static object _getCSharpArgumentInfoArray;
            private static object _setCSharpArgumentInfoArray;
            private static MethodCall<object, object> _getMemberCall;
            private static MethodCall<object, object> _setMemberCall;
            private static bool _init;

            private static void Init()
            {
                if (!_init)
                {
                    var binderType = Type.GetType(BinderTypeName, false);
                    if (binderType == null)
                        throw new InvalidOperationException(
                            "Could not resolve type '{0}'. You may need to add a reference to Microsoft.CSharp.dll to work with dynamic types."
                                .FormatWith(CultureInfo.InvariantCulture, BinderTypeName));

                    // None
                    _getCSharpArgumentInfoArray = CreateSharpArgumentInfoArray(0);
                    // None, Constant | UseCompileTimeType
                    _setCSharpArgumentInfoArray = CreateSharpArgumentInfoArray(0, 3);
                    CreateMemberCalls();

                    _init = true;
                }
            }

            private static object CreateSharpArgumentInfoArray(params int[] values)
            {
                var csharpArgumentInfoType = Type.GetType(CSharpArgumentInfoTypeName);
                var csharpArgumentInfoFlags = Type.GetType(CSharpArgumentInfoFlagsTypeName);

                var a = Array.CreateInstance(csharpArgumentInfoType, values.Length);

                for (var i = 0; i < values.Length; i++)
                {
                    var createArgumentInfoMethod = csharpArgumentInfoType.GetMethod("Create",
                                                                                    BindingFlags.Public |
                                                                                    BindingFlags.Static, null,
                                                                                    new[]
                                                                                        {
                                                                                            csharpArgumentInfoFlags,
                                                                                            typeof (string)
                                                                                        }, null);
                    var arg = createArgumentInfoMethod.Invoke(null, new object[] {0, null});
                    a.SetValue(arg, i);
                }

                return a;
            }

            private static void CreateMemberCalls()
            {
                var csharpArgumentInfoType = Type.GetType(CSharpArgumentInfoTypeName);
                var csharpBinderFlagsType = Type.GetType(CSharpBinderFlagsTypeName);
                var binderType = Type.GetType(BinderTypeName);

                var csharpArgumentInfoTypeEnumerableType = typeof (IEnumerable<>).MakeGenericType(csharpArgumentInfoType);

                var getMemberMethod = binderType.GetMethod("GetMember", BindingFlags.Public | BindingFlags.Static, null,
                                                           new[]
                                                               {
                                                                   csharpBinderFlagsType, typeof (string), typeof (Type)
                                                                   ,
                                                                   csharpArgumentInfoTypeEnumerableType
                                                               }, null);
                _getMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(getMemberMethod);

                var setMemberMethod = binderType.GetMethod("SetMember", BindingFlags.Public | BindingFlags.Static, null,
                                                           new[]
                                                               {
                                                                   csharpBinderFlagsType, typeof (string), typeof (Type)
                                                                   ,
                                                                   csharpArgumentInfoTypeEnumerableType
                                                               }, null);
                _setMemberCall = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(setMemberMethod);
            }

            public static CallSiteBinder GetMember(string name, Type context)
            {
                Init();
                return (CallSiteBinder) _getMemberCall(null, 0, name, context, _getCSharpArgumentInfoArray);
            }

            public static CallSiteBinder SetMember(string name, Type context)
            {
                Init();
                return (CallSiteBinder) _setMemberCall(null, 0, name, context, _setCSharpArgumentInfoArray);
            }
        }

        internal class NoThrowExpressionVisitor : ExpressionVisitor
        {
            internal static readonly object ErrorResult = new object();

            protected override Expression VisitConditional(ConditionalExpression node)
            {
                // if the result of a test is to throw an error, rewrite to result an error result value
                if (node.IfFalse.NodeType == ExpressionType.Throw)
                    return Expression.Condition(node.Test, node.IfTrue, Expression.Constant(ErrorResult));

                return base.VisitConditional(node);
            }
        }

        internal class NoThrowGetBinderMember : GetMemberBinder
        {
            private readonly GetMemberBinder _innerBinder;

            public NoThrowGetBinderMember(GetMemberBinder innerBinder)
                : base(innerBinder.Name, innerBinder.IgnoreCase)
            {
                _innerBinder = innerBinder;
            }

            public override DynamicMetaObject FallbackGetMember(DynamicMetaObject target,
                                                                DynamicMetaObject errorSuggestion)
            {
                var retMetaObject = _innerBinder.Bind(target, new DynamicMetaObject[] {});

                var noThrowVisitor = new NoThrowExpressionVisitor();
                var resultExpression = noThrowVisitor.Visit(retMetaObject.Expression);

                var finalMetaObject = new DynamicMetaObject(resultExpression, retMetaObject.Restrictions);
                return finalMetaObject;
            }
        }

        internal class NoThrowSetBinderMember : SetMemberBinder
        {
            private readonly SetMemberBinder _innerBinder;

            public NoThrowSetBinderMember(SetMemberBinder innerBinder)
                : base(innerBinder.Name, innerBinder.IgnoreCase)
            {
                _innerBinder = innerBinder;
            }

            public override DynamicMetaObject FallbackSetMember(DynamicMetaObject target, DynamicMetaObject value,
                                                                DynamicMetaObject errorSuggestion)
            {
                var retMetaObject = _innerBinder.Bind(target, new[] {value});

                var noThrowVisitor = new NoThrowExpressionVisitor();
                var resultExpression = noThrowVisitor.Visit(retMetaObject.Expression);

                var finalMetaObject = new DynamicMetaObject(resultExpression, retMetaObject.Restrictions);
                return finalMetaObject;
            }
        }
    }
}

#endif