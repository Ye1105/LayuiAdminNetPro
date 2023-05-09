using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiAdminNetCore.AdminModels
{
    public class AdminModuleInfo
    {
        /// <summary>
        /// 菜单栏Id
        /// </summary>
        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        // 描述
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// 父菜单栏Id
        /// </summary>
        [JsonProperty("pId")]
        public Guid? PId { get; set; }

        /// <summary>
        /// 路由
        /// </summary>
        [JsonProperty("route")]
        public string? Route { get; set; }

        /// <summary>
        /// 排序Id
        /// </summary>
        [JsonProperty("order")]
        public int? Order { get; set; } = 0;

        /// <summary>
        /// 菜单栏 0 是  1 否
        /// </summary>
        [JsonProperty("isMenu")]
        public sbyte? IsMenu { get; set; }

        /// <summary>
        /// 0 启用  1 禁用  2 审核中  3 审核失败
        /// </summary>
        [JsonProperty("status")]
        public sbyte Status { get; set; } = (sbyte)Enums.ActionStatus.ENABLE;

        [JsonIgnore]
        [NotMapped]
        public AdminRolePermission? AdminRolePermission { get; set; }
    }
}