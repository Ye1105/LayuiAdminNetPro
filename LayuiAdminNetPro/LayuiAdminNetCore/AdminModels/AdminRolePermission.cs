using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayuiAdminNetCore.AdminModels
{
    public class AdminRolePermission
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonProperty("rId")]
        public Guid RId { get; set; }

        /// <summary>
        /// crud id => 对应四种请求方式
        /// 【create => post】
        /// 【read   => get】
        /// 【update => patch or put】
        /// 【delete => delete】
        /// </summary>
        [JsonProperty("cId")]
        public sbyte CId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        [JsonProperty("mId")]
        public Guid MId { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        [NotMapped]
        [JsonProperty("adminModuleInfo")]
        public AdminRoute? AdminModuleInfo { get; set; } = null;
    }
}