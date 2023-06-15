using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RoleCreateReq
    {
        /// <summary>
        /// 角色Id列表
        /// </summary>
        [JsonProperty("rIds")]
        public List<Guid> RIds { get; set; } = new List<Guid>();
    }
}