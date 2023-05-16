using Newtonsoft.Json;

namespace LayuiAdminNetCore.AdminPages
{
    public abstract class IPagedParams
    {
        /// <summary>
        /// 自动加载 0 关闭  1 开启
        /// </summary>
        [JsonProperty("isAutoLoading")]
        public virtual sbyte IsAutoLoading { get; set; } = 0;

        /// <summary>
        /// 页面索引
        /// </summary>
        [JsonProperty("pageIndex")]
        public virtual int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页面大小
        /// </summary>
        [JsonProperty("pageSize")]
        public virtual int PageSize { get; set; } = 15;

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("sTime")]
        public virtual DateTime? STime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("eTime")]
        public virtual DateTime? ETime { get; set; }
    }
}