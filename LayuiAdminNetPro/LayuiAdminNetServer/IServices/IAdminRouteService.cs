using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetCore.RequstModels;

namespace LayuiAdminNetService.IServices
{
    public interface IAdminRouteService : IService<AdminRoute>
    {
        Task<PagedList<AdminRoute>> QueryPagedAsync(RoutePagedReq req);
    }
}