using Newtonsoft.Json;

namespace LayuiAdminNetCore.AdminPages
{
    public class SystemLogPagedParams : PagedParams
    {
        /// <summary>
        /// 用户Ip
        /// </summary>
        [JsonProperty("ip")]
        public string? Ip { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [JsonProperty("accountName")]
        public string? AccountName { get; set; }

        /// <summary>
        /// 操作人角色
        /// </summary>
        [JsonProperty("rId")]
        public Guid? RId { get; set; }
    }
}