using LayuiAdminNetCore.AdminModels;
using System.Linq.Expressions;

namespace LayuiAdminNetGate.IServices
{
    public interface IRoleModulePermissionService
    {
        /// <summary>
        /// 获取用户的角色列表
        /// </summary>
        /// <param name="express"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<List<AdminAccountRole>> GetAccountRolesAsync(Expression<Func<AdminAccountRole, bool>> express, bool isTrack = true);

        /// <summary>
        /// 获取 role_permission 列表和  module_info 列表
        /// </summary>
        /// <returns></returns>
        Task<AdminRolePermission?> GetRolePermissionAsync();

        /// <summary>
        /// 获取账号
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<AdminAccount> AccountFirstOrDefaultAsync(Expression<Func<AdminAccount, bool>> expression, bool isTrack = true);

        /// <summary>
        /// 更新用户账号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        Task<bool> ModifyAccountAsync(AdminAccount account);
    }
}