using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiAdminNetCore.AdminModels
{
    public class AdminRoleInfo
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        [Key]
        [JsonProperty("rId")]
        public Guid RId { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AdminAccountRole? AdminAccountRole { get; set; }
    }
}