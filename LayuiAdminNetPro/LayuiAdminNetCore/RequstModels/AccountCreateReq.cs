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
        public string Sex { get; set; } = "";

        [JsonProperty("jobStatus")]
        public string JobStatus { get; set; } = "";
    }
}