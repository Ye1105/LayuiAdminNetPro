using LayuiAdminNetCore.AdminModels;

namespace LayuiAdminNetCore.AuthorizationModels
{
    public class AdminModuleRolePermission
    {
        public List<AdminRoute>? AdminRoutes { get; set; }

        public List<AdminRolePermission>? AdminRolePermissions { get; set; }
    }
}