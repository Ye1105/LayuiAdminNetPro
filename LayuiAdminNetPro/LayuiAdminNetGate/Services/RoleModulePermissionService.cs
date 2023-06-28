using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetGate.IServices;
using LayuiAdminNetInfrastructure.IRepositoies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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


        public async Task<List<AdminAccountRole>> GetAccountRolesAsync(Expression<Func<AdminAccountRole, bool>> express, bool isTrack = true)
        {
            return await _base.QueryAsync(express, isTrack);
        }

        public async Task<AdminModuleRolePermission?> GetRolePermissionAsync()
        {
            var data = new AdminModuleRolePermission
            {
                AdminRolePermissions = await _base.EntitiesNoTrack<AdminRolePermission>().ToListAsync(),
                AdminModuleInfos = await _base.EntitiesNoTrack<AdminRoute>().Where(x => x.Status == (sbyte)Status.ENABLE).ToListAsync()
            };
            return data;
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