using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RoleCreateReq
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty("uId")]
        public Guid UId { get; set; }

        /// <summary>
        /// 角色Id列表
        /// </summary>
        [JsonProperty("rIds")]
        public List<Guid> RIds { get; set; } = new List<Guid>();
    }
}