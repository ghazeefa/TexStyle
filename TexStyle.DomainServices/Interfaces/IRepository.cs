using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TexStyle.DomainServices.Interfaces {
    public interface IRepository<T> {
        Task<IList<T>> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        Task<IQueryable<T>> GetAllQueryable(params Expression<Func<T, object>>[] navigationProperties);
        Task<IList<T>> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task<T> GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        Task Add(params T[] items);
        Task Update(params T[] items);
        Task Remove(params T[] items);
        Task<T> GetSingleitem(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        //IList<T> GetReprocessed(params Expression<Func<T, object>>[] navigationProperties);
    }
}
