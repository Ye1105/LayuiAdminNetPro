using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetInfrastructure.IRepositoies;
using Manager.Admin.Server.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class AdminAccountRoleService : IAdminAccountRoleService
    {
        private readonly IBase _base;


        public AdminAccountRoleService(IBase baseSevice)
        {
            _base = baseSevice;
        }

        public Task<int> AddAsync(AdminAccountRole model)
        {
            throw new NotImplementedException();
        }


        public async Task<int> AddRangeAsync(List<AdminAccountRole> list, Guid uId)
        {
            var dic = new Dictionary<object, CrudType>();

            var preList = await _base.QueryAsync<AdminAccountRole>(x => x.UId == uId, isTrack: true, null);
            if (preList is not null && preList.Any())
            {
                foreach (var item in preList)
                {
                    dic.Add(item, CrudType.DELETE);
                }
            }

            if (list is not null && list.Any())
            {
                foreach (var item in list)
                {
                    dic.Add(item, CrudType.CREATE);
                }
            }

            return await _base.BatchTransactionAsync(dic);
        }

        public Task<int> DelAsync(AdminAccountRole model)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DelRangeAsync(Guid uId)
        {
            var preList = await _base.QueryAsync<AdminAccountRole>(x => x.UId == uId, isTrack: true, null);
            return await _base.DelRangeAsync(preList);
        }

        public Task<int> DelRangeAsync(List<AdminAccountRole> list)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminAccountRole?> FirstOrDefaultAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isTrack = true)
        {
            return await _base.FirstOrDefaultAsync(expression, isTrack);
        }

        public async Task<List<AdminAccountRole>> QueryAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isInculdeRoleInfo = false, bool isTrack = true)
        {
            if (isInculdeRoleInfo)
            {
                var query = _base.Entities<AdminAccountRole>()
                    .Where(expression)
                    .Include(x => x.AdminRoleInfo)
                    .AsQueryable();

                return isTrack ? await query.ToListAsync() : await query.AsNoTracking().ToListAsync();
            }
            else
                return await _base.QueryAsync(expression, isTrack);
        }

        public Task<List<AdminAccountRole>> QueryAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isTrack = true, string? orderBy = null)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(AdminAccountRole model)
        {
            return await _base.UpdateAsync(model);
        }

        public Task<int> UpdateRangeAsync(List<AdminAccountRole> list)
        {
            throw new NotImplementedException();
        }
    }
}
