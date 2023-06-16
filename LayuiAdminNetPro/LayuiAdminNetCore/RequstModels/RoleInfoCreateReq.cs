using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RoleInfoCreateReq
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }
}