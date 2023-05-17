using LayuiAdminNetCore.Pages;
using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class AccountPagedRequest : QueryParameters
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonProperty("startTime")]
        public DateTime? StartTime { get; set; }
    }
}
