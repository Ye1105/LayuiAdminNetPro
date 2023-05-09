using HuiAdminNetCore.Enums;
using HuiAdminNetCore.Pages;
using MySqlConnector;
using System.Linq.Expressions;

namespace HuiAdminNetInfrastructure.IRepositoies
{
    public interface IBase
    {
        #region IQueryable

        /// <summary>
        /// IQueryable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> Entities<T>() where T : class;

        /// <summary>
        /// IQueryable AsNoTracking
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> EntitiesNoTrack<T>() where T : class;

        #endregion IQueryable

        #region 添加

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add<T>(T model) where T : class;

        /// <summary>
        /// 添加
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> AddAsync<T>(T model) where T : class;

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public Task<int> AddRangeAsync<T>(IEnumerable<T> collection) where T : class;

        #endregion 添加

        #region 删除

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Del<T>(T model) where T : class;

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">需要删除的实体</param>
        /// <returns></returns>
        public Task<int> DelAsync<T>(T model) where T : class;

        /// <summary>
        /// 根据条件删除(支持批量删除)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="delWhere"></param>
        /// <returns></returns>
        public int Del<T>(Expression<Func<T, bool>> delWhere) where T : class;

        /// <summary>
        /// 根据条件删除(支持批量删除)
        /// </summary>
        /// <param name="delWhere">传入Lambda表达式(生成表达式目录树)</param>
        /// <returns></returns>
        public Task<int> DelAsync<T>(Expression<Func<T, bool>> delWhere) where T : class;

        /// <summary>
        /// 删除集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public Task<int> DelRangeAsync<T>(IEnumerable<T> collection) where T : class;

        #endregion 删除

        #region 修改

        /// <summary>
        /// 单实体修改
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update<T>(T model) where T : class;

        /// <summary>
        /// 批量修改（非lambda）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model">要修改实体中 修改后的属性</param>
        /// <param name="whereLamda">查询实体的条件</param>
        /// <param name="proNames">lambda的形式表示要修改的实体属性名</param>
        /// <returns></returns>
        public int Update<T>(T model, Expression<Func<T, bool>> whereLamda, params string[] proNames) where T : class;

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">修改后的实体</param>
        /// <returns></returns>
        public Task<int> UpdateAsync<T>(T model) where T : class;

        /// <summary>
        /// 批量修改（非lambda）
        /// </summary>
        /// <param name="model">要修改实体中 修改后的属性 </param>
        /// <param name="whereLambda">查询实体的条件</param>
        /// <param name="proNames">lambda的形式表示要修改的实体属性名</param>
        /// <returns></returns>
        public Task<int> UpdateAsync<T>(T model, Expression<Func<T, bool>> whereLambda, params string[] proNames) where T : class;

        /// <summary>
        /// 修改集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public Task<int> UpdateRangeAsync<T>(IEnumerable<T> collection) where T : class;

        #endregion 修改

        #region 查询

        /// <summary>
        ///查询单个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        public T? FirstOrDefault<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true) where T : class;

        /// <summary>
        /// 查询单个是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        public Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true) where T : class;

        /// <summary>
        /// 查询单个是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderByLambda"></param>
        /// <param name="isTrack"></param>
        /// <returns></returns>
        public Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> whereLambda, string orderBy = "", bool isTrack = true) where T : class;

        /// <summary>
        /// 根据条件排序和查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="isTrack"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<T> Query<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true, string? orderBy = null) where T : class;

        /// <summary>
        /// 根据条件查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda">查询条件(lambda表达式的形式生成表达式目录树)</param>
        /// <param name="isTrack">是否跟踪状态，默认是跟踪的</param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true, string? orderBy = null) where T : class;

        /// <summary>
        /// 根据条件排序和查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda">linq查询条件</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="offset">偏移量</param>
        /// <param name="isTrack">跟踪</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> whereLambda, int pageIndex = 1, int pageSize = 15, int offset = 0, bool isTrack = true, string? orderBy = null) where T : class;

        /// <summary>
        /// 根据条件分页排序和查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereLambda">linq查询条件</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="offset">偏移量</param>
        /// <param name="isTrack">跟踪</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        public Task<PagedList<T>> QueryPagedAsync<T>(Expression<Func<T, bool>> whereLambda, int pageIndex = 1, int pageSize = 15, int offset = 0, bool isTrack = true, string? orderBy = null) where T : class;

        #endregion 查询

        #region 存储过程

        /// <summary>
        /// 执行添加,删除,修改操作(或调用存储过程)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params MySqlParameter[] pars);

        /// <summary>
        /// 执行增加,删除,修改操作(或调用存储过程)
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public Task<int> ExecuteSqlAsync(string sql, params MySqlParameter[] pars);

        /// <summary>
        /// 执行查询操作（调用查询类的存储过程）
        /// 注：查询必须返回实体的所有属性字段；结果集中列名必须与属性映射的项目匹配；查询中不能包含关联数据
        /// 除Select以外其他的SQL语句无法执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="isTrack">是否跟踪状态，默认是跟踪的</param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public IQueryable<T> ExecuteQuery<T>(string sql, bool isTrack = true, params MySqlParameter[] pars) where T : class;

        /// <summary>
        /// 执行查询操作
        /// 注：查询必须返回实体的所有属性字段；结果集中列名必须与属性映射的项目匹配；查询中不能包含关联数据
        /// 除Select以外其他的SQL语句无法执行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        ///  <param name="whereLambda">查询条件</param>
        /// <param name="isTrack">是否跟踪状态，默认是跟踪的</param>
        /// <param name="pars"></param>
        /// <returns></returns>
        public IQueryable<T> ExecuteQuery<T>(string sql, Expression<Func<T, bool>> whereLambda, bool isTrack = true, params MySqlParameter[] pars) where T : class;

        #endregion 存储过程

        #region 批量事务

        /// <summary>
        /// 批量执行【事务】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        int BatchTransaction(Dictionary<object, CrudType> keyValuePairs);

        /// <summary>
        /// 批量执行【事务】
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> BatchTransactionAsync(Dictionary<object, CrudType> keyValuePairs);

        #endregion 批量事务
    }
}