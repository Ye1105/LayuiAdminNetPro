using AutoMapper;
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

        /// <summary>
        /// 单个账号
        /// </summary>
        /// <returns></returns>
        [HttpGet("{uId:guid}")]
        public async Task<IActionResult> Single(Guid uId)
        {
            var account = await _admin.FirstOrDefaultAsync(x => x.UId == uId);

            return Ok(Success(new { account }));
        }

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateReq req)
        {
            /*
             * 1.参数校验
             * 2.账号是否存在
             * 3.赋值
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "account");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _admin.FirstOrDefaultAsync(x => x.Name == req.Name || x.Phone == req.Phone);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "用户名或手机号已存在"));
            }

            var acc = new AdminAccount()
            {
                UId = Guid.NewGuid(),
                Name = req.Name,
                Phone = req.Phone,
                Password = req.Password,
                Sex = req.Sex!.Value,
                JobStatus = req.JobStatus!.Value,
                Status = (sbyte)Status.ENABLE,
                Created = DateTime.Now,
                LastLoginIp = null,
                LastLoginTime = null
            };

            var res = await _admin.AddAsync(acc);
            return res > 0 ? Ok(Success()) : Ok(Fail());
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
    }
}