using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class PersonPatchReq
    {
        /// <summary>
        /// Patch 局部修改的方式
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; } = "";


        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; } = "";
    }
}