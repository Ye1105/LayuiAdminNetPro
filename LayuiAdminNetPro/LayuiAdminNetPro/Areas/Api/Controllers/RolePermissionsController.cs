using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.AuthorizationModels;
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
    public class RolePermissionsController : BaseController
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAdminRolePermissionService _permission;

        public RolePermissionsController(IWebHostEnvironment webHostEnvironment, IAdminRolePermissionService adminRolePermissionService)
        {
            _webHostEnvironment = webHostEnvironment;
            _permission = adminRolePermissionService;
        }

        #region Create

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RolePermissionCreateReq req)
        {
            /*
             * 1.参数校验
             * 2.创建对象
             * 3.事务【删除+增加】 删除原有当前角色的权限，新增修改后的权限
             */

            var jsonSchema = await JsonSchemas.GetSchema(_webHostEnvironment, "permission-create");

            var schema = JSchema.Parse(jsonSchema);

            var validate = JObject.Parse(JsonConvert.SerializeObject(req)).IsValid(schema, out IList<string> errorMessages);
            if (!validate)
            {
                return Ok(Fail(errorMessages, "参数错误"));
            }

            //2.创建对象
            var model = new List<AdminRolePermission>();
            if (req.Permisses is not null && req.Permisses.Any())
            {
                foreach (var item in req.Permisses)
                {
                    model.Add(new AdminRolePermission()
                    {
                        CId = item.CId,
                        MId = item.MId,
                        RId = req.RId,
                    });
                }
            }

            //3.事务【删除+增加】 删除原有当前角色的权限，新增修改后的权限

            var res = await _permission.AddRangeAsync(model, req.RId);

            return res > 0 ? Ok(Success("操作成功")) : Ok(Fail("操作失败"));
        }

        #endregion Create
    }
}