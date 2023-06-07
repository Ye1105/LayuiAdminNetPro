using Newtonsoft.Json;

namespace LayuiAdminNetCore.RequstModels
{
    public class AccountPatchReq
    {
        /// <summary>
        /// Patch 局部修改的方式
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; } = "";

        /// <summary>
        /// uId 列表
        /// </summary>
        [JsonProperty("uIds")]
        public List<Guid> UIds { get; set; } = new List<Guid>();

        /// <summary>
        /// 密码
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; } = "";
    }
}
