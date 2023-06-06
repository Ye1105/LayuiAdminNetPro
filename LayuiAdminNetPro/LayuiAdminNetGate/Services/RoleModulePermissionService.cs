using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetGate.IServices;
using LayuiAdminNetInfrastructure.IRepositoies;
using System.Linq.Expressions;

namespace LayuiAdminNetGate.Services
{
    public class RoleModulePermissionService : IRoleModulePermissionService
    {
        private readonly IBase _base;
        public RoleModulePermissionService(IBase baseService)
        {
            _base = baseService;
        }

        public async Task<AdminAccount?> AccountFirstOrDefaultAsync(Expression<Func<AdminAccount, bool>> expression, bool isTrack = true)
        {
            var account = await _base.FirstOrDefaultAsync(expression, isTrack);
            return account;
        }

        public AdminAccount? AccountFirstOrDefaultSync(Expression<Func<AdminAccount, bool>> expression, bool isTrack = true)
        {
            var account = _base.FirstOrDefault(expression, isTrack);
            return account;
        }


        public Task<List<AdminAccountRole>> GetAccountRolesAsync(Expression<Func<AdminAccountRole, bool>> express, bool isTrack = true)
        {
            throw new NotImplementedException();
        }

        public Task<AdminRolePermission?> GetRolePermissionAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ModifyAccountAsync(AdminAccount account)
        {
            return await _base.UpdateAsync(account) > 0;
        }

        public bool ModifyAccountSync(AdminAccount account)
        {
            return _base.Update(account) > 0;
        }
    }
}