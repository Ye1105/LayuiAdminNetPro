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

namespace LayuiAdminNetPro.Areas.Api.Controllers
{
    /// <summary>
    /// 路由管理控制器
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
        private readonly IAdminRolePermissionService _permission;

        public RoutesController(IMapper mapper, IAdminRouteService route, IAdminRolePermissionService permission, IWebHostEnvironment webHostEnvironment)
        {
            _route = route;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
            _permission = permission;
        }

        #region Get

        /// <summary>
        /// 查询父节点
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] Guid rId)
        {
            //路由列表
            //角色权限列表

            var routes = await _route.QueryAsync(x => true, isTrack: false);

            var permissions = await _permission.QueryAsync(x => x.RId == rId, isInculdeModuleInfo: true, isTrack: false);

            return Ok(Success(new { routes, permissions }));
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
                list = _mapper.Map<IEnumerable<DtoAdminRoute>>(list)
            };

            return Ok(Success(JsonData));
        }

        #endregion Get

        #region Post

        /// <summary>
        /// 创建路由
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

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "route-schema");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _route.FirstOrDefaultAsync(x => x.Name == req.Name && x.Route == req.Route, isTrack: false);
            if (exsit != null)
            {
                return Ok(Fail("名称已存在"));
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

        #endregion Post

        #region Put

        /// <summary>
        /// 编辑路由
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] RoutePutReq req)
        {
            /*
             * 1.参数校验【合法性校验】
             * 2.账号是否存在【重复性校验】
             * 3.赋值 | 修改
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "route-schema");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            var exsit = await _route.FirstOrDefaultAsync(x => x.Name == req.Name.Trim() && x.Id != req.Id);
            if (exsit != null)
            {
                return Ok(Fail(errorMessages, "名称已存在"));
            }

            if (req.Route != "/")
            {
                exsit = await _route.FirstOrDefaultAsync(x => x.Route == req.Route.Trim() && x.Id != req.Id);
                if (exsit != null)
                {
                    return Ok(Fail(errorMessages, "路由已存在"));
                }
            }

            var adminRoute = await _route.FirstOrDefaultAsync(x => x.Id == req.Id, isTrack: true);
            if (adminRoute != null)
            {
                adminRoute.Name = req.Name;
                adminRoute.Route = req.Route;
                adminRoute.Order = req.Order;
                adminRoute.Status = (sbyte)EnumDescriptionAttribute.GetEnumByDescription<Status>(req.Status);
                var res = await _route.UpdateAsync(adminRoute);
                return res > 0 ? Ok(Success()) : Ok(Fail());
            }
            else
            {
                return Ok(Fail("路由信息不存在"));
            }
        }

        #endregion Put
    }
}