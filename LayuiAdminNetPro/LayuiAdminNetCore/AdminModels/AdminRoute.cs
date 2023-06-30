﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LayuiAdminNetCore.AdminModels
{
    public class AdminRoute
    {
        /// <summary>
        /// 菜单栏Id
        /// </summary>
        [Key]
        [JsonProperty("id")]
        public Guid Id { get; set; }

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
        /// 名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 排序Id
        /// </summary>
        [JsonProperty("order")]
        public int? Order { get; set; } = 0;


        /// <summary>
        /// 是否是菜单栏界面（默认不是）
        /// </summary>
        [JsonProperty("menu")]
        public sbyte Menu { get; set; } = (sbyte)Enums.Status.DISABLE;

        /// <summary>
        /// 0 禁用  1 启用  2 审核中  3 审核失败
        /// </summary>
        [JsonProperty("status")]
        public sbyte Status { get; set; } = (sbyte)Enums.Status.ENABLE;

        [JsonIgnore]
        [NotMapped]
        public AdminRolePermission? AdminRolePermission { get; set; }
    }
}