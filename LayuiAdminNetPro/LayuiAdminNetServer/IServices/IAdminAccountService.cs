using LayuiAdminNetCore.AdminModels;
using System.Linq.Expressions;

namespace LayuiAdminNetServer.IServices
{
    public interface IAdminAccountService
    {
        /// <summary>
        /// 查询账号信息
        /// </summary>
        /// <param name="phone">账号[手机号]</param>
        /// <param name="password">密码</param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<AdminAccount?> FirstOrDefaultAsync(string phone, string password, bool isTrack = true);

        /// <summary>
        /// 查询账号信息
        /// </summary>
        /// <param name="expression"> linq 条件</param>
        /// <param name="isIncludeAccountRoles">是否关联角色关系表,默认不关联</param>
        /// <param name="isTrack">默认：追踪</param>
        /// <returns></returns>
        Task<AdminAccount?> FirstOrDefaultAsync(Expression<Func<AdminAccount, bool>> expression, bool isIncludeAccountRoles = false, bool isTrack = true);

    }
}
