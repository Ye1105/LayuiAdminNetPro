using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class AccountCreateReq
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("phone")]
        public string Phone { get; set; } = "";

        [JsonProperty("password")]
        public string Password { get; set; } = "";

        [JsonProperty("sex")]
        public sbyte? Sex { get; set; } = null;

        [JsonProperty("jobStatus")]
        public sbyte? JobStatus { get; set; } = null;
    }
}
