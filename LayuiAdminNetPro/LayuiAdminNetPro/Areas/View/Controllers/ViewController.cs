using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Web;

namespace LayuiAdminNetPro.Areas.View.Controllers
{
    public class ViewController : Controller
    {
        /// <summary>
        ///  create a view to list function
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public virtual IActionResult Index()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create a view to add function
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("create")]
        public virtual IActionResult Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create a view to edit function
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("edit")]
        public virtual IActionResult Edit()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// create a view to delete function
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("{id:guid}/delete")]
        public virtual IActionResult Delete(Guid id)
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