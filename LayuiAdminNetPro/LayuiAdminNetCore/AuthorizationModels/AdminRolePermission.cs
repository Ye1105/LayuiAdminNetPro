using LayuiAdminNetCore.AdminModels;

namespace LayuiAdminNetCore.AuthorizationModels
{
    public class AdminRolePermission
    {
        public List<AdminModuleInfo>? AdminModuleInfos { get; set; }

        public List<AdminModels.AdminRolePermission>? AdminRolePermissions { get; set; }
    }
}