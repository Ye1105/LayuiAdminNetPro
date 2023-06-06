using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class AccountEditReq
    {
        [JsonProperty("uId")]
        public Guid UId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("phone")]
        public string Phone { get; set; } = "";

        [JsonProperty("sex")]
        public string Sex { get; set; } = "";

        [JsonProperty("jobStatus")]
        public string JobStatus { get; set; } = "";

        [JsonProperty("status")]
        public string Status { get; set; } = "";
    }
}
