using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetService.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class AdminRolePermissionService : IAdminRolePermissionService
    {
        private readonly IBase _base;

        public AdminRolePermissionService(IBase baseSevice)
        {
            _base = baseSevice;
        }

        public Task<int> AddAsync(AdminRolePermission model)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(List<AdminRolePermission> list, Guid uId)
        {
            throw new NotImplementedException();
        }

        public Task<int> DelAsync(AdminRolePermission model)
        {
            throw new NotImplementedException();
        }

        public Task<int> DelRangeAsync(List<AdminRolePermission> list)
        {
            throw new NotImplementedException();
        }

        public Task<AdminRolePermission?> FirstOrDefaultAsync(Expression<Func<AdminRolePermission, bool>> expression, bool isTrack = true)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AdminRolePermission>> QueryAsync(Expression<Func<AdminRolePermission, bool>> expression, bool isTrack = true, string? orderBy = null)
        {
            return await _base.QueryAsync(expression, isTrack, orderBy);
        }

        public async Task<List<AdminRolePermission>> QueryAsync(Expression<Func<AdminRolePermission, bool>> expression, bool isInculdeModuleInfo = false, bool isTrack = true)
        {
            if (isInculdeModuleInfo)
            {
                var query = _base.Entities<AdminRolePermission>()
                    .Where(expression)
                    .Include(x => x.AdminRoute)
                    .Where(t => t.AdminRoute!.Status == (sbyte)Status.ENABLE)
                    .AsQueryable();

                return isTrack ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
            }
            else
                return await _base.QueryAsync(expression, isTrack);
        }


        public Task<int> UpdateAsync(AdminRolePermission model)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(List<AdminRolePermission> list)
        {
            throw new NotImplementedException();
        }
    }
}