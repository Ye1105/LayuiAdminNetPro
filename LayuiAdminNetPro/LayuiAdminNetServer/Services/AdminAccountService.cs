using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AdminPages;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetServer.IServices;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;

namespace LayuiAdminNetServer.Services
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly IBase _base;


        public AdminAccountService(IBase baseSevice)
        {
            _base = baseSevice;
        }

        public async Task<AdminAccount?> FirstOrDefaultAsync(string phone, string password, bool isTrack = true)
        {

            password = Md5Helper.MD5(password);

            var account = await _base.FirstOrDefaultAsync<AdminAccount>((x => x.Password == password && x.Phone == phone), isTrack);

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

        public IPagedList<AdminAccount> GetPageList(IPagedParams para, bool isTrack = true)
        {
            var pagedParams = para as AccountPagedParams;

            var query = isTrack ? _base.Entities<AdminAccount>() : _base.EntitiesNoTrack<AdminAccount>();

            if (pagedParams.STime != null)
            {
                query = query.Where(x => x.Created >= pagedParams.STime.Value);
            }

            if (pagedParams.ETime != null)
            {
                query = query.Where(x => x.Created < pagedParams.ETime.Value.AddDays(1));
            }

            if (!string.IsNullOrWhiteSpace(pagedParams.Query))
            {
                query = query.Where(x => x.Name.StartsWith(pagedParams.Query) || x.Name.EndsWith(pagedParams.Query) || x.Name.Contains(pagedParams.Query));
            }

            query = query.OrderByDescending(x => x.Created);

            var data = query.ToPagedList(pagedParams.PageIndex, pagedParams.PageSize);

            return data;
        }
    }
}
