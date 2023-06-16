using LayuiAdminNetCore.AdminModels;
using System.Linq.Expressions;

namespace Manager.Admin.Server.IServices
{
    public interface IAdminAccountRoleService
    {
        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<AdminAccountRole?> FirstOrDefaultAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isTrack = true);

        /// <summary>
        /// GetListByAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<List<AdminAccountRole>> QueryAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isInculdeRoleInfo = false, bool isTrack = true);

        /// <summary>
        /// AddRangeAsync
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(IEnumerable<AdminAccountRole> collection, Guid uId);


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        Task<int> DelRangeAsync(Guid uId);
    }
}