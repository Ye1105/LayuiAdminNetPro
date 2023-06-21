using LayuiAdminNetCore.AdminModels;
using System.Linq.Expressions;

namespace LayuiAdminNetService.IServices
{
    public interface IAdminRolePermissionService : IService<AdminRolePermission>
    {
        Task<List<AdminRolePermission>> QueryAsync(Expression<Func<AdminRolePermission, bool>> expression, bool isInculdeModuleInfo = false, bool isTrack = true);
    }
}