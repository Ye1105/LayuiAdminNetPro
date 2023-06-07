using AutoMapper;
using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Collections.Generic;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    [ApiController]
    [Authorize(Policy = Policys.Admin)]
    [Route($"{nameof(Api)}/[controller]")]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class AccountsController : BaseController
    {
        private readonly IAdminAccountService _admin;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountsController(IAdminAccountService adminAccountService, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _admin = adminAccountService;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        #region Get

        /// <summary>
        /// 单个账号
        /// </summary>
        /// <returns></returns>
        [HttpGet("{uId:guid}")]
        public async Task<IActionResult> Single(Guid uId)
        {
            var account = await _admin.FirstOrDefaultAsync(x => x.UId == uId);

            var dtoAccount = _mapper.Map<DtoAdminAccount>(account);

            return Ok(Success(new { account = dtoAccount }));
        }

        /// <summary>
        /// 账号列表【分页】
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] AccountPagedReq req)
        {
            var list = await _admin.QueryPagedAsync(req);

            var JsonData = new
            {
                list.TotalPages,
                list.CurrentPage,
                list.PageSize,
                list.TotalCount,
                //AutoMapper 映射
                list = _mapper.Map<IEnumerable<DtoAdminAccount>>(list)
            };

            return Ok(Success(JsonData));
        }

        #endregion Get

        #region Create

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateReq req)
        {
            /*
             * 1.参数校验【合法性校验】
             * 2.账号是否存在【重复性校验】
             * 3.赋值 | 创建
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "account-create");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _admin.FirstOrDefaultAsync(x => x.Name == req.Name);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "用户名已存在"));
            }

            exsit = await _admin.FirstOrDefaultAsync(x => x.Phone == req.Phone);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "手机号已存在"));
            }


            var acc = new AdminAccount()
            {
                UId = Guid.NewGuid(),
                Name = req.Name,
                Phone = req.Phone,
                Password = req.Password,
                Sex = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<SexStatus>(req.Sex),
                JobStatus = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<JobStatus>(req.JobStatus),
                Status = (sbyte)Status.ENABLE,
                Created = DateTime.Now,
                LastLoginIp = null,
                LastLoginTime = null
            };

            var res = await _admin.AddAsync(acc);
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
        public async Task<IActionResult> Put([FromBody] AccountPutReq req)
        {
            /*
             * 1.参数校验【合法性校验】
             * 2.账号是否存在【重复性校验】
             * 3.赋值 | 修改
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "account-put");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _admin.FirstOrDefaultAsync(x => x.Name == req.Name && x.UId != req.UId);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "用户名已存在"));
            }


            exsit = await _admin.FirstOrDefaultAsync(x => x.Phone == req.Phone && x.UId != req.UId);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "手机号已存在"));
            }


            var account = await _admin.FirstOrDefaultAsync(x => x.UId == req.UId, isTrack: true);
            if (account != null)
            {
                account.Name = req.Name;
                account.Phone = req.Phone;
                account.Sex = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<SexStatus>(req.Sex);
                account.JobStatus = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<JobStatus>(req.JobStatus);
                account.Status = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<Status>(req.Status);
                var res = await _admin.UpdateAsync(account);
                return res > 0 ? Ok(Success()) : Ok(Fail());
            }
            else
            {
                return Ok(Fail("账号不存在"));
            }
        }

        #endregion Put


        #region Patch

        /// <summary>
        /// 按需修改账号信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] AccountPatchReq req)
        {
            /**
             * 1.参数校验
             */
            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "account-patch");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            #region 锁定 | 解锁账号
            if (req.Method == "锁定" || req.Method == "解锁")
            {
                if (req.UIds.Any())
                {
                    var list = await _admin.QueryAsync(x => req.UIds.Contains(x.UId), isTrack: true);
                    if (list != null && list.Any())
                    {
                        foreach (var item in list)
                        {
                            item.Status = req.Method == "锁定" ? (sbyte)Status.DISABLE : (sbyte)Status.ENABLE;
                        }
                        var res = await _admin.UpdateRangeAsync(list);
                        return res > 0 ? Ok(Success()) : Ok(Fail());
                    }
                    return Ok(Fail("没有匹配的数据"));
                }
                return Ok(Fail("uId不能为空"));
            }
            #endregion

            #region 修改密码
            if (req.Method == "修改密码")
            {
                if (!string.IsNullOrWhiteSpace(req.Password))
                {
                    var account = await _admin.FirstOrDefaultAsync(x => req.UIds.Contains(x.UId), isTrack: true);
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
            #endregion

            return Ok(Fail());
        }
        #endregion
    }
}