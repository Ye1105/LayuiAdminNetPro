using AutoMapper;
using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Manager.Admin.Server.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json;

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    [ApiController]
    [Authorize(Policy = Policys.Admin)]
    [Route($"{nameof(Api)}/[controller]")]
    [TypeFilter(typeof(CustomLogAsyncActionFilterAttribute))]
    public class RoutesController : BaseController
    {
        private readonly IAdminRouteService _route;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper _mapper;

        public RoutesController(IMapper mapper, IAdminRouteService route, IWebHostEnvironment webHostEnvironment)
        {
            _route = route;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }


        #region Get

        /// <summary>
        /// 查询父节点
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Guid pId)
        {
            var list = await _route.QueryAsync(x => x.PId == pId, isTrack: false);

            return Ok(Success(new { list }));
        }


        /// <summary>
        /// 路由列表【分页】
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpGet("paged")]
        public async Task<IActionResult> Paged([FromQuery] RoutePagedReq req)
        {
            var list = await _route.QueryPagedAsync(req);

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

        /// <summary>
        /// 创建账号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RouteCreateReq req)
        {
            /*
             * 1.参数校验【合法性校验】
             * 2.账号是否存在【重复性校验】
             * 3.赋值 | 创建
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "route-create");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _route.FirstOrDefaultAsync(x => x.Name == req.Name, isTrack: false);
            if (exsit != null)
            {
                return Ok(Fail("名称已存在"));
            }

            exsit = await _route.FirstOrDefaultAsync(x => x.Route == req.Route, isTrack: false);
            if (exsit != null)
            {
                return Ok(Fail("路由已存在"));
            }

            var acc = new AdminRoute()
            {
                Id = Guid.NewGuid(),
                PId = req.PId,
                Route = req.Route.Trim().ToLower(),
                Name = req.Name,
                Order = req.Order,
                Status = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<Status>(req.Status),
            };

            var res = await _route.AddAsync(acc);
            return res > 0 ? Ok(Success()) : Ok(Fail());
        }
    }
}