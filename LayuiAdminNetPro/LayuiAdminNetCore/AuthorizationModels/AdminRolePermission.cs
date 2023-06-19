using LayuiAdminNetCore.AdminModels;

namespace LayuiAdminNetCore.AuthorizationModels
{
    public class AdminRolePermission
    {
        public List<AdminRoute>? AdminModuleInfos { get; set; }

        public List<AdminModels.AdminRolePermission>? AdminRolePermissions { get; set; }
    }
}