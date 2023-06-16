using AutoMapper;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Manager.Admin.Server.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    [ApiController]
    [Authorize(Policy = Policys.Admin)]
    [Route($"{nameof(Api)}/[controller]")]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class RoleInfosController : BaseController
    {
        private readonly IAdminAccountService _admin;
        private readonly IAdminRoleInfoService _roleInfo;
        private readonly IMapper _mapper;
        private readonly IAdminAccountRoleService _accrole;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RoleInfosController(IAdminAccountService accountService, IAdminAccountRoleService adminAccountRoleService, IAdminRoleInfoService adminRoleInfoService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _admin = accountService;
            _roleInfo = adminRoleInfoService;
            _accrole = adminAccountRoleService;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }

        #region Get

        /// <summary>
        /// 账号列表【分页】
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public async Task<IActionResult> Paged([FromQuery] RoleInfoPagedReq req)
        {
            var list = await _roleInfo.QueryPagedAsync(req);

            var JsonData = new
            {
                list.TotalPages,
                list.CurrentPage,
                list.PageSize,
                list.TotalCount,
                list
            };

            return Ok(Success(JsonData));
        }

        #endregion Get

        #region Create

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] string name)
        {
            /*
             * 1.参数校验【合法性校验】
             * 2.角色名称是否存在【重复性校验】
             * 3.赋值 | 创建
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "role-create");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(new { name })).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _roleInfo.FirstOrDefaultAsync(x => x.Name == name.Trim());
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "角色名称已存在"));
            }

            var roleInfo = new AdminRoleInfo()
            {
                RId = Guid.NewGuid(),
                Name = name,
            };

            var res = await _roleInfo.AddAsync(roleInfo);
            return res > 0 ? Ok(Success()) : Ok(Fail());
        }

        #endregion Create

        #region Put

        /// <summary>
        /// 编辑账号
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RoleInfoPutReq req)
        {
            /*
             * 1.参数校验【合法性校验】
             * 2.账号是否存在【重复性校验】
             * 3.赋值 | 修改
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "role-edit");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _roleInfo.FirstOrDefaultAsync(x => x.Name == req.Name.Trim() && x.RId != req.RId);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "角色名已存在"));
            }

            var roleInfo = await _roleInfo.FirstOrDefaultAsync(x => x.RId == req.RId, isTrack: true);
            if (roleInfo != null)
            {
                roleInfo.Name = req.Name;
                var res = await _roleInfo.UpdateAsync(roleInfo);
                return res > 0 ? Ok(Success()) : Ok(Fail());
            }
            else
            {
                return Ok(Fail("角色不存在"));
            }
        }

        #endregion Put
    }
}