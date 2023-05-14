using LayuiAdminNetCore.Database.EF;
using LayuiAdminNetInfrastructure.IRepositoies;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Data;
using System.Data.Common;
using System.Dynamic;

namespace LayuiAdminNetInfrastructure.Repositoies
{
    public class ProcedureRepository : IProcedure
    {
        private readonly LayuiAdminContext _db;

        public ProcedureRepository(LayuiAdminContext db)
        {
            this._db = db;
        }

        public async Task<IEnumerable<dynamic>> ExecSqlAsync(string sql, MySqlParameter[]? mySqlParameters = null)
        {
            var connection = _db.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                _db.Database.OpenConnection();
            }
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (mySqlParameters != null && mySqlParameters.Any()) cmd.Parameters.AddRange(mySqlParameters);

            DbDataReader dr = await cmd.ExecuteReaderAsync();
            var list = new List<DynamicEntity>();
            while (await dr.ReadAsync())
            {
                DynamicEntity entity = new();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    entity.SetMember(dr.GetName(i), dr.GetValue(i));
                }
                list.Add(entity);
            }
            dr.Dispose();
            return list;
        }

        public async Task<IEnumerable<dynamic>> ExecSpAsync(string procedure, MySqlParameter[]? mySqlParameters = null)
        {
            var connection = _db.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                _db.Database.OpenConnection();
            }
            using var cmd = connection.CreateCommand();
            cmd.CommandText = procedure;
            cmd.CommandType = CommandType.StoredProcedure;
            if (mySqlParameters != null) cmd.Parameters.AddRange(mySqlParameters);

            DbDataReader dr = await cmd.ExecuteReaderAsync();
            var list = new List<DynamicEntity>();
            while (await dr.ReadAsync())
            {
                DynamicEntity entity = new();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    entity.SetMember(dr.GetName(i), dr.GetValue(i));
                }
                list.Add(entity);
            }
            dr.Dispose();
            return list;
        }

        public IEnumerable<dynamic> ExecSpSync(string sql, MySqlParameter[]? mySqlParameters = null)
        {
            var connection = _db.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                _db.Database.OpenConnection();
            }
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.StoredProcedure;
            if (mySqlParameters != null) cmd.Parameters.AddRange(mySqlParameters);

            DbDataReader dr = cmd.ExecuteReader();
            var list = new List<DynamicEntity>();
            while (dr.Read())
            {
                DynamicEntity entity = new();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    entity.SetMember(dr.GetName(i), dr.GetValue(i));
                }
                list.Add(entity);
            }
            dr.Dispose();
            return list;
        }

        public IEnumerable<dynamic> ExecSqlSync(string sql, MySqlParameter[]? mySqlParameters = null)
        {
            var connection = _db.Database.GetDbConnection();
            if (connection.State == ConnectionState.Closed)
            {
                _db.Database.OpenConnection();
            }
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (mySqlParameters != null) cmd.Parameters.AddRange(mySqlParameters);

            DbDataReader dr = cmd.ExecuteReader();
            var list = new List<DynamicEntity>();
            while (dr.Read())
            {
                DynamicEntity entity = new();
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    entity.SetMember(dr.GetName(i), dr.GetValue(i));
                }
                list.Add(entity);
            }
            dr.Dispose();
            return list;
        }

        /// <summary>
        /// 动态实体
        /// </summary>
        internal class DynamicEntity : DynamicObject
        {
            /// <summary>
            /// 属性和值的字典表
            /// </summary>
            private readonly Dictionary<string, object?> values = new(StringComparer.OrdinalIgnoreCase);

            public override bool TryGetMember(GetMemberBinder binder, out object? result)
            {
                if (values.ContainsKey(binder.Name))
                {
                    result = values[binder.Name];
                }
                else
                {
                    throw new MissingMemberException($"The property {binder.Name} does not exis");
                }
                return true;
            }

            public override bool TrySetMember(SetMemberBinder binder, object? value)
            {
                SetMember(binder.Name, value);
                return true;
            }

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return values.Keys;
            }

            internal void SetMember(string propertyName, object? value)
            {
                if (object.ReferenceEquals(value, DBNull.Value))
                {
                    values[propertyName] = null;
                }
                else
                {
                    values[propertyName] = value;
                }
            }
        }
    }
}