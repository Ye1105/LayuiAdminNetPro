using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetGate.IServices;
using System.Linq.Expressions;

namespace LayuiAdminNetGate.Services
{
    public class RoleModulePermissionService : IRoleModulePermissionService
    {
        public Task<AdminAccount> AccountFirstOrDefaultAsync(Expression<Func<AdminAccount, bool>> expression, bool isTrack = true)
        {
            throw new NotImplementedException();
        }

        public Task<List<AdminAccountRole>> GetAccountRolesAsync(Expression<Func<AdminAccountRole, bool>> express, bool isTrack = true)
        {
            throw new NotImplementedException();
        }

        public Task<AdminRolePermission?> GetRolePermissionAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ModifyAccountAsync(AdminAccount account)
        {
            throw new NotImplementedException();
        }
    }
}