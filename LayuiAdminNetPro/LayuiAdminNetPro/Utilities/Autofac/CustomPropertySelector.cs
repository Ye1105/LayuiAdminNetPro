using Autofac.Core;
using System.Reflection;

namespace LayuiAdminNetPro.Utilities.Autofac
{
    internal class CustomPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            //在这里就是判断哪些属性是需要做属性注入的
            var res = propertyInfo.CustomAttributes.Any(a => a.AttributeType == typeof(CustomSelectAttribute));
            return res;
        }
    }
}