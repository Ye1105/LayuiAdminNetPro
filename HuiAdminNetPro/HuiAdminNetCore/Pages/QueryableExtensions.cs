using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace HuiAdminNetCore.Pages
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string? orderBy = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            //遍历获取当前实例的属性
            Type t = typeof(T);
            List<string> proNames = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).Select(x => x.Name).ToList();
            if (proNames == null || !proNames.Any())
            {
                return source;
            }

            var orderByAfterSplit = orderBy.Split(',');

            StringBuilder orderString = new("");
            //"created desc,id desc"
            foreach (var orderByClause in orderByAfterSplit) //reverse 反转
            {
                //"created desc" "id desc"
                var trimmedOrderByClause = orderByClause.Trim();

                var orderDescending = trimmedOrderByClause.EndsWith(" desc");

                var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");

                var propertyName = indexOfFirstSpace == -1 ? trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

                if (string.IsNullOrWhiteSpace(propertyName))
                {
                    throw new ArgumentNullException(propertyName);
                }

                var proName = proNames.Find(x => x.Contains(propertyName, StringComparison.OrdinalIgnoreCase));

                if (!string.IsNullOrWhiteSpace(proName))
                {
                    orderString.Append(proName + (orderDescending ? " descending," : " ascending,"));
                }
            }

            if (!string.IsNullOrWhiteSpace(orderString.ToString()))
            {
                source = source.OrderBy(orderString.ToString().TrimEnd(',', ' '));
            }

            return source;
        }
    }
}