using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LayuiAdminNetCore.AdminModels
{
    public class AdminLogTencentSMS
    {
        [Key]
        [JsonProperty("requestId")]
        public Guid RequestId { get; set; }

        [JsonProperty("sendStatusSet")]
        public string? SendStatusSet { get; set; }

        /// <summary>
        /// sms
        /// </summary>
        [JsonProperty("sms")]
        public string? Sms { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [JsonProperty("created")]
        public DateTime? Created { get; set; }
    }
}