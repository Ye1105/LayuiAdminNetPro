using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetServer.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LayuiAdminNetServer.Services
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly IBase _base;


        public AdminAccountService(IBase baseSevice)
        {
            _base = baseSevice;
        }

        public async Task<AdminAccount?> FirstOrDefaultAsync(string name, string password, bool isTrack = true)
        {

            password = Md5Helper.MD5(password);

            var account = await _base.FirstOrDefaultAsync<AdminAccount>((x => x.Password == password && x.Name == name), isTrack);

            return account;
        }

        public async Task<AdminAccount?> FirstOrDefaultAsync(Expression<Func<AdminAccount, bool>> expression, bool isIncludeAccountRoles = false, bool isTrack = true)
        {
            if (!isIncludeAccountRoles)
            {
                return await _base.FirstOrDefaultAsync(expression, isTrack);
            }
            else
            {
                //关联 账号角色关系表

                var query = _base.Entities<AdminAccount>()
                    .Where(expression)
                    .Include(x => x.AdminAccountRoles)
                    .AsQueryable();

                return isTrack ? await query.FirstOrDefaultAsync() : await query.AsNoTracking().FirstOrDefaultAsync();
            }
        }
    }
}
