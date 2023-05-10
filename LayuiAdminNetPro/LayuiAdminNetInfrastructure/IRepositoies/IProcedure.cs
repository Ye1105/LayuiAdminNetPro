using MySqlConnector;

namespace LayuiAdminNetInfrastructure.IRepositoies
{
    public interface IProcedure
    {
        /// <summary>
        ///  存储过程返回动态实体集合
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        IEnumerable<dynamic> ExecSpSync(string procedure, MySqlParameter[]? mySqlParameters = null);

        /// <summary>
        /// 存储过程返回动态实体集合
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> ExecSpAsync(string procedure, MySqlParameter[]? mySqlParameters = null);

        /// <summary>
        /// SQL过程返回映射实体集合
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        IEnumerable<dynamic> ExecSqlSync(string sql, MySqlParameter[]? mySqlParameters = null);

        /// <summary>
        /// SQL过程返回映射实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="mySqlParameters"></param>
        /// <returns></returns>
        Task<IEnumerable<dynamic>> ExecSqlAsync(string sql, MySqlParameter[]? mySqlParameters = null);
    }
}