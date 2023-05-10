using LayuiAdminNetCore.Enums;
using Newtonsoft.Json;

namespace LayuiAdminNetCore.AdminPages
{
    public class ColumnPagedParams : PagedParams
    {
        //public override int PageSize { get; set; } = 5;

        /// <summary>
        /// 查询keys
        /// </summary>
        [JsonProperty("query")]
        public string? Query { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("status")]
        public ActionStatus? Status { get; set; }
    }
}