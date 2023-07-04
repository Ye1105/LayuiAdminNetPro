namespace LayuiAdminNetService.IServices
{
    public interface IMemoryService
    {
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="compile">前置条件，满足前置条件再删除</param>
        /// <param name="key"></param>
        void Remove(bool preCondition, object key);
    }
}
