using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities
{
    /// <summary>
    ///     EntityBase
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        ///     返回对象原始Json字符串
        /// </summary>
        /// <returns>json</returns>
        public override string ToString() {
            //return base.ToString();

            return JsonConvert.SerializeObject(this);
        }
    }
}