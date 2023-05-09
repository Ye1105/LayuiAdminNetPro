using HuiAdminNetCore.AdminModels;

namespace HuiAdminNetCore.AuthorizationModels
{
    public class AdminRolePermission
    {
        public List<AdminModuleInfo>? AdminModuleInfos { get; set; }

        public List<AdminModels.AdminRolePermission>? AdminRolePermissions { get; set; }
    }
}