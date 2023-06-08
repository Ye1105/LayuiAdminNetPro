using LayuiAdminNetCore.AdminModels;
using System.Linq.Expressions;

namespace LayuiAdminNetService.IServices
{
    public interface IAdminRoleInfoService
    {
        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<AdminRoleInfo?> FirstOrDefaultAsync(Expression<Func<AdminRoleInfo, bool>> expression, bool isTrack = true);

        /// <summary>
        /// GetListByAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isInculdeModuleInfo"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<List<AdminRoleInfo>> QueryAsync(Expression<Func<AdminRoleInfo, bool>> expression, bool isTrack = true);

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddAsync(AdminRoleInfo model);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(AdminRoleInfo model);

        /// <summary>
        /// DelAsync
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> DelRangeAsync(IEnumerable<AdminRoleInfo> models);
    }
}