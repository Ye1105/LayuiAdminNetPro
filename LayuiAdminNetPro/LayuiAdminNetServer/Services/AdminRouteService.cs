using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Constants;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetService.IServices;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class AdminRouteService : IAdminRouteService
    {
        private readonly IBase _base;
        private readonly IMemoryService _cache;

        public AdminRouteService(IBase baseService, IMemoryService memoryService)
        {
            _base = baseService;
            _cache = memoryService;
        }

        public async Task<int> AddAsync(AdminRoute model)
        {
            var res = await _base.AddAsync(model);
            //清缓存
            _cache.Remove(res > 0, Constants.ROLE_PERMISSION_CACHE);
            return res;
        }

        public Task<int> AddRangeAsync(List<AdminRoute> list, Guid uId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DelAsync(AdminRoute model)
        {
            var res = await _base.DelAsync(model);
            //清缓存
            _cache.Remove(res > 0, Constants.ROLE_PERMISSION_CACHE);
            return res;
        }

        public Task<int> DelRangeAsync(List<AdminRoute> list)
        {
            throw new NotImplementedException();
        }

        public async Task<AdminRoute?> FirstOrDefaultAsync(Expression<Func<AdminRoute, bool>> expression, bool isTrack = true)
        {
            return await _base.FirstOrDefaultAsync(expression, isTrack);
        }

        public async Task<List<AdminRoute>> QueryAsync(Expression<Func<AdminRoute, bool>> expression, bool isTrack = true, string? orderBy = null)
        {
            var list = await _base.QueryAsync(expression, isTrack, orderBy);

            return list;
        }

        public async Task<PagedList<AdminRoute>> QueryPagedAsync(RoutePagedReq req)
        {
            Expression<Func<AdminRoute, bool>> whereLambda = x => true;

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                whereLambda = whereLambda.And(x => x.Name == req.Name);
            }

            if (req.PId is not null)
            {
                whereLambda = whereLambda.And(x => x.PId == req.PId);
            }

            whereLambda = whereLambda.And(x => x.Status == (sbyte)Status.ENABLE);

            var data = await _base.QueryPagedAsync(whereLambda, req.PageIndex, req.PageSize, req.OffSet, isTrack: false, req.OrderBy);

            return data;
        }

        public async Task<int> UpdateAsync(AdminRoute model)
        {
            var res = await _base.UpdateAsync(model);
            //清缓存
            _cache.Remove(res > 0, Constants.ROLE_PERMISSION_CACHE);
            return res;
        }

        public Task<int> UpdateRangeAsync(List<AdminRoute> list)
        {
            throw new NotImplementedException();
        }
    }
}