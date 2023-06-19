using System.Linq.Expressions;

namespace LayuiAdminNetService.IServices
{
    public interface IService<T> where T : class
    {
        /// <summary>
        /// FirstOrDefaultAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, bool isTrack = true);

        /// <summary>
        /// GetListByAsync
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="isInculdeModuleInfo"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> expression, bool isTrack = true, string? orderBy = null);

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddAsync(T model);

        /// <summary>
        /// AddRangeAsync
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="uId"></param>
        /// <returns></returns>
        Task<int> AddRangeAsync(List<T> list, Guid uId);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(T model);

        /// <summary>
        /// 编辑账号集合
        /// </summary>
        /// <param name="adminAccount"></param>
        /// <returns></returns>
        Task<int> UpdateRangeAsync(List<T> list);


        /// <summary>
        /// DelAsync
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> DelAsync(T model);

        /// <summary>
        /// DelAsync
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        Task<int> DelRangeAsync(List<T> list);
    }
}