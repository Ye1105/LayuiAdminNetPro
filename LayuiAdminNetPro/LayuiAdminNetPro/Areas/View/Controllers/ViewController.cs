using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    public class ViewController : Controller
    {
        /// <summary>
        ///  (list)普通数据 视图，
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Index()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分页数据 视图
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("menu")]
        public virtual IActionResult Menu()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建功能 视图
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("create")]
        public virtual IActionResult Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 编辑功能 视图
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("edit")]
        public virtual IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除功能 视图
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("delete")]
        public virtual IActionResult Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 解析querystring参数，转为字典格式
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected Dictionary<string, string> GetQueryString(HttpContext context)
        {
            var dic = new Dictionary<string, string>();
            var param = HttpUtility.UrlDecode(context.Request.QueryString.ToString(), Encoding.UTF8);
            if (!string.IsNullOrWhiteSpace(param))
            {
                param = param.Trim();
                if (param.StartsWith("?"))
                    param = param.Remove(0, 1);
                var arr = param.Split('&');
                if (arr.Length > 0)
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        var item = arr[i];
                        var splitItem = item.Split('=');
                        dic.Add(splitItem[0], splitItem[1]);
                    }
                }
                return dic;
            }
            return dic;
        }
    }
}