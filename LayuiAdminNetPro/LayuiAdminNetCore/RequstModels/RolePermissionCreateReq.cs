using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RolePermissionCreateReq
    {
        [JsonProperty("permisses")]
        public ICollection<AdminRolePermissionRequestModel>? Permisses { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonProperty("rId")]
        public Guid RId { get; set; }
    }

    public class AdminRolePermissionRequestModel
    {
        [JsonProperty("cId")]
        public sbyte CId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        [JsonProperty("mId")]
        public Guid MId { get; set; }
    }
}