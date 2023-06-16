using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetCore.RequstModels;
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

        public async Task<PagedList<AdminRoleInfo>> QueryPagedAsync(RoleInfoPagedReq req)
        {
            Expression<Func<AdminRoleInfo, bool>> whereLambda = x => true;


            if (req.RId != null)
            {
                whereLambda = whereLambda.And(x => x.RId == req.RId);
            }

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                whereLambda = whereLambda.And(x => x.Name == req.Name);
            }

            var data = await _base.QueryPagedAsync(whereLambda, req.PageIndex, req.PageSize, req.OffSet, isTrack: false, req.OrderBy);

            return data;
        }

        public async Task<int> UpdateAsync(AdminRoleInfo model)
        {
            return await _base.UpdateAsync(model);
        }
    }
}
