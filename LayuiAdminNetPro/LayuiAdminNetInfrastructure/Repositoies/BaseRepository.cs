using LayuiAdminNetCore.Database.EF;
using LayuiAdminNetCore.Enums;
using LayuiAdminNetCore.Pages;
using LayuiAdminNetInfrastructure.IRepositoies;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Linq.Expressions;
using System.Reflection;

namespace LayuiAdminNetInfrastructure.Repositoies
{
    public class BaseRepository : IBase
    {
        private readonly LayuiAdminContext _db;

        public BaseRepository(LayuiAdminContext db)
        {
            _db ??= db;
        }

        #region IQueryable

        public IQueryable<T> Entities<T>() where T : class
        {
            return _db.Set<T>();
        }

        public IQueryable<T> EntitiesNoTrack<T>() where T : class
        {
            return _db.Set<T>().AsNoTracking();
        }

        #endregion IQueryable

        #region 添加

        public int Add<T>(T model) where T : class
        {
            _db.Entry(model).State = EntityState.Added;
            return _db.SaveChanges();
        }

        public async Task<int> AddAsync<T>(T model) where T : class
        {
            await _db.AddAsync(model);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync<T>(IEnumerable<T> collection) where T : class
        {
            _db.AddRange(collection);
            return await _db.SaveChangesAsync();
        }

        #endregion 添加

        #region 删除

        public int Del<T>(T model) where T : class
        {
            _db.Entry(model).State = EntityState.Deleted;
            return _db.SaveChanges();
        }

        public async Task<int> DelAsync<T>(T model) where T : class
        {
            _db.Entry(model).State = EntityState.Deleted;
            return await _db.SaveChangesAsync();
        }

        public int Del<T>(Expression<Func<T, bool>> delWhere) where T : class
        {
            var data = Entities<T>().Where(delWhere);
            _db.RemoveRange(data);
            return _db.SaveChanges();
        }

        public async Task<int> DelAsync<T>(Expression<Func<T, bool>> delWhere) where T : class
        {
            var data = Entities<T>().Where(delWhere);
            _db.RemoveRange(data);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> DelRangeAsync<T>(IEnumerable<T> collection) where T : class
        {
            _db.RemoveRange(collection);
            return await _db.SaveChangesAsync();
        }

        #endregion 删除

        #region 修改

        public int Update<T>(T model) where T : class
        {
            _db.Entry(model).State = EntityState.Modified;
            return _db.SaveChanges();
        }

        public async Task<int> UpdateAsync<T>(T model) where T : class
        {
            _db.Entry(model).State = EntityState.Modified;
            return await _db.SaveChangesAsync();
        }

        public int Update<T>(T model, Expression<Func<T, bool>> whereLamda, params string[] proNames) where T : class
        {
            var listModifies = Entities<T>().Where(whereLamda).ToList();
            Type t = typeof(T);
            //实体类上的属性列表
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            //要修改的属性
            Dictionary<string, PropertyInfo> dicPros = new();
            proInfos.ForEach(p =>
            {
                if (proNames.Contains(p.Name))
                {
                    dicPros.Add(p.Name, p);
                }
            });
            foreach (string proName in proNames)
            {
                if (dicPros.ContainsKey(proName))
                {
                    PropertyInfo proInfo = dicPros[proName];
                    object? newValue = proInfo.GetValue(model, null);
                    foreach (T m in listModifies)
                    {
                        proInfo.SetValue(m, newValue, null);
                    }
                }
            }
            return _db.SaveChanges();
        }

        public async Task<int> UpdateAsync<T>(T model, Expression<Func<T, bool>> whereLambda, params string[] proNames) where T : class
        {
            var listModifes = await Entities<T>().Where(whereLambda).ToListAsync();
            Type t = typeof(T);
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            Dictionary<string, PropertyInfo> dicPros = new();
            proInfos.ForEach(p =>
            {
                if (proNames.Contains(p.Name))
                {
                    dicPros.Add(p.Name, p);
                }
            });
            foreach (string proName in proNames)
            {
                if (dicPros.ContainsKey(proName))
                {
                    PropertyInfo proInfo = dicPros[proName];
                    object? newValue = proInfo.GetValue(model, null);
                    foreach (T m in listModifes)
                    {
                        proInfo.SetValue(m, newValue, null);
                    }
                }
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync<T>(IEnumerable<T> collection) where T : class
        {
            _db.UpdateRange(collection);
            return await _db.SaveChangesAsync();
        }

        #endregion 修改

        #region 查询

        public T? FirstOrDefault<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true) where T : class
        {
            var query = Entities<T>().Where(whereLambda);

            return isTrack ? query.FirstOrDefault() : query.AsNoTracking().FirstOrDefault();
        }

        public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true) where T : class
        {
            if (isTrack)
            {
                return await Entities<T>().FirstOrDefaultAsync(whereLambda);
            }
            else
            {
                return await Entities<T>().Where(whereLambda).AsNoTracking().FirstOrDefaultAsync();
            }
        }

        public async Task<T?> FirstOrDefaultAsync<T>(Expression<Func<T, bool>> whereLambda, string orderBy = "", bool isTrack = true) where T : class
        {
            var query = Entities<T>().Where(whereLambda);

            if (!isTrack)
            {
                query = query.AsNoTracking();
            }

            query = query.ApplySort(orderBy);

            return await query.FirstOrDefaultAsync();
        }

        public List<T> Query<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true, string? orderBy = null) where T : class
        {
            var query = Entities<T>().Where(whereLambda);

            if (!isTrack)
            {
                query = query.AsNoTracking();
            }

            query = query.ApplySort(orderBy);

            return query.ToList();
        }

        public async Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> whereLambda, bool isTrack = true, string? orderBy = null) where T : class
        {
            var query = Entities<T>().Where(whereLambda);

            if (!isTrack)
            {
                query = query.AsNoTracking();
            }

            query = query.ApplySort(orderBy);

            return await query.ToListAsync();
        }

        public async Task<List<T>> QueryAsync<T>(Expression<Func<T, bool>> whereLambda, int pageIndex = 1, int pageSize = 15, int offset = 0, bool isTrack = true, string? orderBy = null) where T : class
        {
            var query = Entities<T>().Where(whereLambda);

            if (!isTrack)
            {
                query = query.AsNoTracking();
            }

            query = query.ApplySort(orderBy);

            return await PagedList<T>.CreateAsync(query, pageIndex, pageSize, offset);
        }

        public async Task<PagedList<T>> QueryPagedAsync<T>(Expression<Func<T, bool>>? whereLambda, int pageIndex = 1, int pageSize = 15, int offset = 0, bool isTrack = true, string? orderBy = null) where T : class
        {

            var query = Entities<T>();

            if (whereLambda != null)
                query = query.Where(whereLambda);

            if (!isTrack)
            {
                query = query.AsNoTracking();
            }

            query = query.ApplySort(orderBy);

            return await PagedList<T>.CreateAsync(query, pageIndex, pageSize, offset);
        }

        #endregion 查询

        #region 存储过程

        public IQueryable<T> ExecuteQuery<T>(string sql, bool isTrack = true, params MySqlParameter[] pars) where T : class
        {
            var query = _db.Set<T>().FromSqlRaw(sql, pars);

            if (!isTrack)
            {
                return query.AsNoTracking();
            }

            return query;
        }

        public IQueryable<T> ExecuteQuery<T>(string sql, Expression<Func<T, bool>> whereLambda, bool isTrack = true, params MySqlParameter[] pars) where T : class
        {
            var query = _db.Set<T>().FromSqlRaw(sql, pars).Where(whereLambda);

            if (!isTrack)
            {
                return query.AsNoTracking();
            }

            return query;
        }

        public int ExecuteSql(string sql, params MySqlParameter[] pars)
        {
            return _db.Database.ExecuteSqlRaw(sql, pars);
        }

        public async Task<int> ExecuteSqlAsync(string sql, params MySqlParameter[] pars)
        {
            return await _db.Database.ExecuteSqlRawAsync(sql, pars);
        }

        #endregion 存储过程

        #region 批量事务

        public int BatchTransaction(Dictionary<object, CrudType> keyValuePairs)
        {
            if (keyValuePairs.Count == 0)
                return 0;

            using var transaction = _db.Database.BeginTransaction();

            var createRange = keyValuePairs.Where(x => x.Value == CrudType.CREATE);
            if (createRange is not null && createRange.Any())
            {
                var list = createRange.Select(x => x.Key).ToList();
                _db.AddRange(list);
            }

            var updateRange = keyValuePairs.Where(x => x.Value == CrudType.UPDATE);
            if (updateRange is not null && updateRange.Any())
            {
                var list = updateRange.Select(x => x.Key).ToList();
                _db.UpdateRange(list);
            }

            var deleteRange = keyValuePairs.Where(x => x.Value == CrudType.DELETE);
            if (deleteRange is not null && deleteRange.Any())
            {
                var list = deleteRange.Select(x => x.Key).ToList();
                _db.RemoveRange(list);
            }

            var res = _db.SaveChanges();
            if (res > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
            return res;
        }

        public async Task<int> BatchTransactionAsync(Dictionary<object, CrudType> keyValuePairs)
        {
            if (keyValuePairs.Count == 0)
                return 0;

            using var transaction = _db.Database.BeginTransaction();

            var createRange = keyValuePairs.Where(x => x.Value == CrudType.CREATE);
            if (createRange is not null && createRange.Any())
            {
                var list = createRange.Select(x => x.Key).ToList();
                _db.AddRange(list);
            }

            var updateRange = keyValuePairs.Where(x => x.Value == CrudType.UPDATE);
            if (updateRange is not null && updateRange.Any())
            {
                var list = updateRange.Select(x => x.Key).ToList();
                _db.UpdateRange(list);
            }

            var deleteRange = keyValuePairs.Where(x => x.Value == CrudType.DELETE);
            if (deleteRange is not null && deleteRange.Any())
            {
                var list = deleteRange.Select(x => x.Key).ToList();
                _db.RemoveRange(list);
            }

            var res = await _db.SaveChangesAsync();
            if (res > 0)
            {
                transaction.Commit();
            }
            else
            {
                transaction.Rollback();
            }
            return res;
        }

        #endregion 批量事务
    }
}