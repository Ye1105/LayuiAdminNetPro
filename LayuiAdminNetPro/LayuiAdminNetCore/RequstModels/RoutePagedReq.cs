using LayuiAdminNetCore.Pages;
using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class RoutePagedReq : QueryParameters
    {
        [JsonProperty("name")]
        public string? Name { get; set; } = "";
    }
}