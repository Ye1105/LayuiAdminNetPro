using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayuiAdminNetCore.AdminModels
{
    public class AdminAccountRole
    {
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        [JsonProperty("uId")]
        public Guid UId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        [JsonProperty("rId")]
        public Guid RId { get; set; }

        /// <summary>
        /// 账号表
        /// </summary>
        [NotMapped]
        [JsonProperty("adminAccount")]
        public AdminAccount? AdminAccount { get; set; }

        /// <summary>
        /// 角色详情
        /// </summary>
        [NotMapped]
        [JsonProperty("adminRoleInfo")]
        public AdminRoleInfo? AdminRoleInfo { get; set; }
    }
}