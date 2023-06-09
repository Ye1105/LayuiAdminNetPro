﻿using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetCore.RequstModels;

namespace LayuiAdminNetService.IServices
{
    public interface IAdminRoleInfoService : IService<AdminRoleInfo>
    {
        Task<PagedList<AdminRoleInfo>> QueryPagedAsync(RoleInfoPagedReq req);
    }
}