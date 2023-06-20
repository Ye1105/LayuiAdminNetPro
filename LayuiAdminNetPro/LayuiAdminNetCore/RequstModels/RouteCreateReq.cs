using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RouteCreateReq
    {
        /// <summary>
        /// 父节点
        /// </summary>
        [JsonProperty("pId")]
        public Guid PId { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        [JsonProperty("route")]
        public string Route { get; set; } = "";

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        /// <summary>
        /// 排序
        /// </summary>
        [JsonProperty("order")]
        public int Order { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; } = "";
    }
}