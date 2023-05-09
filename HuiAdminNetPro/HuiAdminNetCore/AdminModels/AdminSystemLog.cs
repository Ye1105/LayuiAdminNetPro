using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HuiAdminNetCore.AdminModels
{
    public class AdminSystemLog
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        [Key]
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// 当前操作用户的Id
        /// </summary>
        [JsonProperty("uId")]
        public Guid UId { get; set; }

        /// <summary>
        /// 当前操作用户的Ip
        /// </summary>
        [JsonProperty("ip")]
        public string? Ip { get; set; }

        /// <summary>
        /// 日志对应的路由路径
        /// </summary>
        [JsonProperty("route")]
        public string? Route { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [NotMapped]
        [JsonProperty("adminAccount")]
        public AdminAccount? AdminAccount { get; set; } = null;
    }
}