using Microsoft.EntityFrameworkCore;
using TexStyle.DomainServices.Interfaces;
using TexStyle.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.DomainServices.Implementation {
    internal class Repository<T> : IRepository<T> where T : class {

        private readonly AppDbContext _db;
        public Repository(AppDbContext db) {
            _db = db;
        }

        //public Repository() {
        //    _db = new AppDbContext();
        //}

        public virtual async Task Add(params T[] items) {
            using (var commit = _db.Database.BeginTransaction()) {
                try {
                    foreach (T item in items) {
                        var prop = item.GetType().GetProperty("CreatedOn");
                        if (prop != null) {
                            item.GetType().GetProperty("CreatedOn").SetValue(item, DateTime.UtcNow);
                        }
                        _db.Entry(item).State = EntityState.Added;
                    }
                    await _db.SaveChangesAsync();
                    commit.Commit();
                } catch (Exception ex) {
                    commit.Rollback();
                    throw ex;
                }
            }
        }

        public virtual async Task Update(params T[] items) {
            using (var commit = _db.Database.BeginTransaction()) {
                try {
                    foreach (T item in items) {
                        var prop = item.GetType().GetProperty("UpdatedOn");
                        if (prop != null) {
                            item.GetType().GetProperty("UpdatedOn").SetValue(item, DateTime.UtcNow);
                        }
                        _db.Entry(item).State = EntityState.Modified;
                    }
                    await _db.SaveChangesAsync();
                    commit.Commit();
                } catch (Exception ex) {
                    commit.Rollback();
                    throw ex;
                }
            }
        }

        public virtual async Task Remove(params T[] items) {
            using (var commit = _db.Database.BeginTransaction()) {
                try {
                    foreach (T item in items) {
                        _db.Entry(item).State = EntityState.Deleted;
                    }
                    await _db.SaveChangesAsync();
                    commit.Commit();
                } catch (Exception ex) {
                    commit.Rollback();
                    throw ex;
                }
            }
            
        }

        public virtual async Task<IList<T>> GetAll(params Expression<Func<T, object>>[] navigationProperties) {
            List<T> list;
            IQueryable<T> dbQuery = _db.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties) {
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            }

            list = dbQuery
                .AsNoTracking()
                .ToList<T>();

            return await Task.FromResult(list);
        }

        public virtual async Task<IQueryable<T>> GetAllQueryable(params Expression<Func<T, object>>[] navigationProperties) {
            IQueryable<T> dbQuery = _db.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            return await Task.FromResult(dbQuery);
        }

        public virtual async Task<IList<T>> GetList(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties) {
            List<T> list;
            IQueryable<T> dbQuery = _db.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            list = dbQuery
                .AsNoTracking()
                .Where(where)
                .ToList<T>();
            return await Task.FromResult(list);
        }

        public virtual async Task<T> GetSingle(Func<T, bool> where,
             params Expression<Func<T, object>>[] navigationProperties) {
            T item = null;
            IQueryable<T> dbQuery = _db.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            return await Task.FromResult(item);
        }

        public virtual async Task<T> GetSingleitem(Func<T, bool> where,
      params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;
            IQueryable<T> dbQuery = _db.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            return await Task.FromResult(item);
        }


        public virtual async Task<bool> GetSetData()
        {
            try
            {
            _db.Database.ExecuteSqlCommand("GetData");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

    }
}
