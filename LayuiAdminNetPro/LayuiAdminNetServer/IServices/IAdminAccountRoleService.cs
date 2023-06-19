using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetService.IServices;
using System.Linq.Expressions;

namespace Manager.Admin.Server.IServices
{
    public interface IAdminAccountRoleService : IService<AdminAccountRole>
    {
        /// <summary>
        /// GetListByAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<List<AdminAccountRole>> QueryAsync(Expression<Func<AdminAccountRole, bool>> expression, bool isInculdeRoleInfo = false, bool isTrack = true);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="uId"></param>
        /// <returns></returns>
        Task<int> DelRangeAsync(Guid uId);
    }
}