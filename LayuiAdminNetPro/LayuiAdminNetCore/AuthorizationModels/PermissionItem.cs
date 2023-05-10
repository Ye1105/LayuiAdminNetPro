namespace LayuiAdminNetCore.AuthorizationModels
{
    public class PermissionItem
    {
        /// <summary>
        /// 角色
        /// </summary>
        public virtual string? Role { get; set; }

        /// <summary>
        /// 请求Url
        /// </summary>
        public virtual string? Url { get; set; }
    }
}