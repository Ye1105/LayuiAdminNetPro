using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiAdminNetCore.AdminModels
{
    public class AdminAccount
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        [Key]
        [JsonProperty("uId")]
        public Guid UId { get; set; }

        /// <summary>
        /// 账号名称
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("sex")]
        public sbyte Sex { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        [JsonIgnore]
        public string? Password { get; set; }

        /// <summary>
        /// 最近一次的登录时间
        /// </summary>
        [JsonProperty("lastLoginTime")]
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 最近一次的登录IP
        /// </summary>
        [JsonProperty("lastLoginIp")]
        public string? LastLoginIp { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        /// <summary>
        /// 0 在职  1 离职  2 出差  3 休假
        /// </summary>
        [JsonProperty("isOnJob")]
        public sbyte JobStatus { get; set; } = (sbyte)Enums.JobStatus.ON_JOB;

        /// <summary>
        /// 0 启用  1 禁用  2 审核中  3 审核失败
        /// </summary>
        [JsonProperty("status")]
        public sbyte Status { get; set; } = (sbyte)Enums.ActionStatus.ENABLE;

        [NotMapped]
        [JsonIgnore]
        [JsonProperty("adminAccountRoles")]
        public IList<AdminAccountRole>? AdminAccountRoles { get; set; } = new List<AdminAccountRole>();

        [NotMapped]
        [JsonIgnore]
        [JsonProperty("adminSystemLog")]
        public AdminSystemLog? AdminSystemLog { get; set; }
    }
}