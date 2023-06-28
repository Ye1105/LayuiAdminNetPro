using LayuiAdminNetCore.AdminModels;
using System.Linq.Expressions;

namespace LayuiAdminNetService.IServices
{
    public interface IAdminRolePermissionService : IService<AdminRolePermission>
    {
        Task<List<AdminRolePermission>> QueryAsync(Expression<Func<AdminRolePermission, bool>> expression, bool isInculdeModuleInfo = false, bool isTrack = true);


        /// <summary>
        /// AddRangeAsync
        /// </summary>
        /// <typeparam name="AdminRolePermission"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<AdminRolePermission> collection, Guid rId);
    }
}