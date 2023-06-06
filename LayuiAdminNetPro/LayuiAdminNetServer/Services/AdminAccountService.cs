using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetInfrastructure.IRepositoies;
using LayuiAdminNetService.IServices;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Ocsp;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class AdminAccountService : IAdminAccountService
    {
        private readonly IBase _base;


        public AdminAccountService(IBase baseSevice)
        {
            _base = baseSevice;
        }


        public async Task<int> UpdateAsync(AdminAccount adminAccount)
        {
            return await _base.UpdateAsync(adminAccount);
        }

        public async Task<int> AddAsync(AdminAccount adminAccount)
        {

            adminAccount.Password = Md5Helper.MD5(adminAccount.Password!);

            return await _base.AddAsync(adminAccount);
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

        public async Task<PagedList<AdminAccount>> QueryPagedAsync(AccountPagedReq req)
        {
            Expression<Func<AdminAccount, bool>> whereLambda = x => true;

            if (req.StartTime != null)
            {
                whereLambda = whereLambda.And(x => x.Created >= req.StartTime);
            }

            if (req.EndTime != null)
            {
                whereLambda = whereLambda.And(x => x.Created < req.EndTime);
            }

            if (req.UId != null)
            {
                whereLambda = whereLambda.And(x => x.UId == req.UId);
            }

            if (!string.IsNullOrWhiteSpace(req.Name))
            {
                whereLambda = whereLambda.And(x => x.Name == req.Name);
            }

            if (!string.IsNullOrWhiteSpace(req.Phone))
            {
                whereLambda = whereLambda.And(x => x.Phone == req.Phone);
            }

            if (req.Sex != null)
            {
                whereLambda = whereLambda.And(x => x.Sex == req.Sex);
            }

            var data = await _base.QueryPagedAsync(whereLambda, req.PageIndex, req.PageSize, req.OffSet, isTrack: false, req.OrderBy);

            return data;
        }
    }
}
