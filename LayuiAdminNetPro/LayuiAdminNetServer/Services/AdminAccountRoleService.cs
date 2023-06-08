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

        public async Task<int> AddRangeAsync(IEnumerable<AdminAccountRole> collection, Guid uId)
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

            if (collection is not null && collection.Any())
            {
                foreach (var item in collection)
                {
                    dic.Add(item, CrudType.CREATE);
                }
            }

            return await _base.BatchTransactionAsync(dic);
        }

        public async Task<AdminAccountRole?> FirstOrDefaultAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isTrack = true)
        {
            var mdoel = await _base.FirstOrDefaultAsync(expression, isTrack);
            return mdoel;
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
    }
}
