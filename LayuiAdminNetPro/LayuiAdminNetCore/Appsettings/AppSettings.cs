using Newtonsoft.Json;

namespace LayuiAdminNetCore.Appsettings
{
    /// <summary>
    /// 对应appsettings中的App节点的配置信息
    /// </summary>
    public class AppSettings
    {
        public JwtConfig? JwtBearer { set; get; }

        public TencentSmsConfig? TencentSms { set; get; }

        public SmsConfig? Sms { get; set; }

        public MailConfig? Mail { get; set; }

        public string? CrosAddress { get; set; }

        public byte ServerStatus { get; set; }

        public string? ConnectionString { get; set; }
    }

    public class MailConfig
    {
        /// <summary>
        /// POP3/SMPT 授权码
        /// </summary>
        public string? AuthorizatioCode { get; set; }

        /// <summary>
        /// 发送邮件的账号主体
        /// </summary>
        public string? MailAccount { get; set; }

        public string? DisplayName { get; set; }

        public string? Host { get; set; }
    }

    public class SmsConfig
    {
        /// <summary>
        /// 每个用户每日最大发送短信数
        /// </summary>
        public int DayLimit { get; set; }
    }

    public class TencentSmsConfig
    {
        [JsonProperty("secretId")]
        public string? SecretId { get; set; }

        [JsonProperty("secretKey")]
        public string? SecretKey { get; set; }

        [JsonProperty("smsSdkAppId")]
        public string? SmsSdkAppId { get; set; }

        [JsonProperty("signName")]
        public string? SignName { get; set; }

        [JsonProperty("templateId")]
        public string? TemplateId { get; set; }
    }

    public class JwtConfig
    {
        /// <summary>
        /// AccessToken密钥
        /// </summary>
        [JsonProperty("securityKey")]
        public string SecurityKey { get; set; } = "";

        /// <summary>
        /// RefreshToken密钥
        /// </summary>
        [JsonProperty("refreshSecurityKey")]
        public string? RefreshSecurityKey { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        [JsonProperty("issuer")]
        public string? Issuer { get; set; }

        /// <summary>
        /// 受众人
        /// </summary>
        [JsonProperty("audience")]
        public string? Audience { get; set; }

        [JsonProperty("accessExpiration")]
        public int AccessExpiration { get; set; }

        [JsonProperty("refreshExpiration")]
        public int RefreshExpiration { get; set; }
    }
}