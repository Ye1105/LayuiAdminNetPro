using Newtonsoft.Json;

namespace LayuiAdminNetCore.AuthorizationModels
{
    public class Authenticate
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("uId")]
        public Guid UId { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("vip")]
        public sbyte Vip { get; set; }

    }
}
