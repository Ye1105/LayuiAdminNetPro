using Microsoft.AspNetCore.Mvc;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    public class ViewController : Controller
    {
        /// <summary>
        ///【创建功能】路由模板
        /// </summary>
        protected const string _create = "create";

        /// <summary>
        ///【更新功能】路由模板
        /// </summary>
        protected const string _patch = "${id:guid}/patch";

        /// <summary>
        ///【删除功能】路由模板
        /// </summary>
        protected const string _delete = "${id:guid}/delete";

        /// <summary>
        ///  create a view to list function
        /// </summary>
        /// <returns></returns>
        public virtual IActionResult Index()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create a view to add function
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IActionResult Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create a view to patch function
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IActionResult Patch()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create a view to delete function
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public virtual IActionResult Delete()
        {
            throw new NotImplementedException();
        }
    }
}