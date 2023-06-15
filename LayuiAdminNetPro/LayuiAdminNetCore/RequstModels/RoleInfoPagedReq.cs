using LayuiAdminNetCore.Pages;
using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RoleInfoPagedReq : QueryParameters
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [JsonProperty("rId")]
        public Guid? RId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }
    }
}