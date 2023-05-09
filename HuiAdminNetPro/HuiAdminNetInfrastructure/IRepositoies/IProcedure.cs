using MySqlConnector;

namespace HuiAdminNetInfrastructure.IRepositoies
{
    public interface IProcedure
    {
        /// <summary>
        /// 存储过程返回映射实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        Task<IList<T>> ExecSpAsync<T>(string sql, MySqlParameter[]? mySqlParameters = null) where T : new();

        /// <summary>
        /// SQL过程返回映射实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> ExecSqlAsync(string sql, MySqlParameter[]? mySqlParameters = null);

        /// <summary>
        /// 存储过程返回动态实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> ExecSpAsync(string sql, MySqlParameter[]? mySqlParameters = null);

        /// <summary>
        ///  存储过程返回动态实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        IEnumerable<dynamic> ExecSpSync(string sql, MySqlParameter[]? mySqlParameters = null);

        IEnumerable<dynamic> ExecSqlSync(string sql, MySqlParameter[]? mySqlParameters = null);
    }
}