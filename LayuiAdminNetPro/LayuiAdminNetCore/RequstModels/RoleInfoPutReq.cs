using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RoleInfoPutReq
    {
        [JsonProperty("rId")]
        public Guid RId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }
}
