using AutoMapper;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.RequstModels;
using LayuiAdminNetPro.Utilities.Filters;
using LayuiAdminNetService.IServices;
using Manager.Admin.Server.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        private readonly IMapper _mapper;

        public RoutesController(IMapper mapper, IAdminRouteService route)
        {
            _route = route;
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
    }
}