using Newtonsoft.Json;

namespace LayuiAdminNetCore.AdminPages
{
    public class AccountPagedParams : PagedParams
    {
        /// <summary>
        /// 查询keys
        /// </summary>
        [JsonProperty("query")]
        public string? Query { get; set; }
    }
}