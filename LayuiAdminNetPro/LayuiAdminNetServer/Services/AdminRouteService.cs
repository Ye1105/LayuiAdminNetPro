using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetService.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class AdminRouteService : IAdminRouteService
    {
        private readonly IBase _base;

        public AdminRouteService(IBase baseService)
        {
            _base = baseService;
        }

        public async Task<int> AddAsync(AdminRoute model)
        {
            return await _base.AddAsync(model);
        }

        public Task<int> AddRangeAsync(List<AdminRoute> list, Guid uId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DelAsync(AdminRoute model)
        {
            return await _base.DelAsync(model);
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
            return await _base.UpdateAsync(model);
        }

        public Task<int> UpdateRangeAsync(List<AdminRoute> list)
        {
            throw new NotImplementedException();
        }
    }
}