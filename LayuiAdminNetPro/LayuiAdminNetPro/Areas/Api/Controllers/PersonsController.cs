using AutoMapper;
using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Data;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 用户接口
    /// </summary>
    [ApiController]
    [Authorize(Policy = Policys.Admin)]
    [Route($"{nameof(Api)}/[controller]")]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class PersonsController : BaseController
    {
        private readonly IAdminAccountService _admin;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAdminRoleInfoService _adminRoleInfoService;

        public PersonsController(IAdminAccountService adminAccountService, IMapper mapper, IWebHostEnvironment webHostEnvironment, IAdminRoleInfoService adminRoleInfoService)
        {
            _admin = adminAccountService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _adminRoleInfoService = adminRoleInfoService;
        }

        #region Get

        /// <summary>
        /// 单个账号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Single()
        {
            var uId = base.UId;

            //账号
            var account = await _admin.FirstOrDefaultAsync(x => x.UId == uId, isIncludeAccountRoles: true, isTrack: false);
            var dtoAccount = _mapper.Map<DtoAdminAccount>(account);
            var roleInfos = new List<AdminRoleInfo>() { };
            if (account != null && account.AdminAccountRoles != null && account.AdminAccountRoles.Any())
            {
                var rIds = account!.AdminAccountRoles!.Select(x => x.RId).ToList();

                if (rIds != null && rIds.Any())
                {
                    roleInfos = await _adminRoleInfoService.QueryAsync(x => rIds.Contains(x.RId), false);
                }
            }

            return Ok(Success(new { account = dtoAccount, roleInfos }));
        }

        #endregion Get

        #region Patch

        /// <summary>
        /// 按需修改账号信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] PersonPatchReq req)
        {
            /**
             * 1.参数校验
             */
            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "person-psd");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            #region 修改密码

            if (req.Method == "修改密码")
            {
                if (!string.IsNullOrWhiteSpace(req.Password))
                {
                    var account = await _admin.FirstOrDefaultAsync(x => x.UId == UId, isTrack: true);
                    if (account != null)
                    {
                        account.Password = Md5Helper.MD5(req.Password);
                        var res = await _admin.UpdateAsync(account);
                        return res > 0 ? Ok(Success()) : Ok(Fail());
                    }
                    return Ok(Fail("没有匹配的数据"));
                }
                return Ok(Fail("密码不能为空"));
            }

            #endregion 修改密码

            return Ok(Fail());
        }

        #endregion Patch
    }
}