using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetService.IServices;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class AdminRoleInfoService : IAdminRoleInfoService
    {
        private readonly IBase _base;

        public AdminRoleInfoService(IBase baseService)
        {
            _base = baseService;
        }

        public async Task<int> AddAsync(AdminRoleInfo model)
        {
            return await _base.AddAsync(model);
        }

        public async Task<int> DelRangeAsync(IEnumerable<AdminRoleInfo> models)
        {
            return await _base.DelRangeAsync(models);
        }

        public async Task<AdminRoleInfo?> FirstOrDefaultAsync(Expression<Func<AdminRoleInfo, bool>> expression, bool isTrack = true)
        {
            var adminRole = await _base.FirstOrDefaultAsync(expression, isTrack);
            return adminRole;
        }

        public async Task<List<AdminRoleInfo>> QueryAsync(Expression<Func<AdminRoleInfo, bool>> expression, bool isTrack = true)
        {
            return await _base.QueryAsync(expression, isTrack);
        }

        public async Task<int> UpdateAsync(AdminRoleInfo model)
        {
            return await _base.UpdateAsync(model);
        }
    }
}
