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

        public async Task<List<AdminRolePermission>> QueryAsync(Expression<Func<AdminRolePermission, bool>> expression, Expression<Func<AdminRolePermission, bool>> expression2, bool isInculdeModuleInfo = false, bool isTrack = true)
        {
            if (isInculdeModuleInfo)
            {
                var query = _base.Entities<AdminRolePermission>()
                    .Where(expression)
                    .Include(x => x.AdminRoute)
                    .Where(expression2)
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

        public async Task<int> AddRangeAsync(IEnumerable<AdminRolePermission> collection, Guid rId)
        {
            var dic = new Dictionary<object, CrudType>();

            var preList = await _base.QueryAsync<AdminRolePermission>(x => x.RId == rId, isTrack: true, "id desc");
            if (preList is not null && preList.Any())
            {
                foreach (var item in preList)
                {
                    dic.Add(item, CrudType.DELETE);
                }
            }

            if (collection is not null && collection.Any())
            {
                foreach (var item in collection)
                {
                    dic.Add(item, CrudType.CREATE);
                }
            }

            var res = await _base.BatchTransactionAsync(dic);

            return res;

        }
    }
}