using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Pages;
using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class AccountPagedRequest : QueryParameters
    {

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("uId")]
        public Guid? UId { get; set; }

        /// <summary>
        /// 账号名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        public sbyte? Sex { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonProperty("endTime")]
        public DateTime? EndTime { get; set; }
    }
}
