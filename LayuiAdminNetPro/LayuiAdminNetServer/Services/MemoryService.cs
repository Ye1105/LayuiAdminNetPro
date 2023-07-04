using LayuiAdminNetService.IServices;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace LayuiAdminNetService.Services
{
    public class MemoryService : IMemoryService
    {
        private readonly IMemoryCache _cache;

        public MemoryService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }
        public void Remove(bool preCondition, object key)
        {
            //删除角色列表缓存
            if (preCondition)
                _cache.Remove(key);
        }
    }
}
