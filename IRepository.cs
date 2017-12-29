
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fit.Repositories
{
    public interface IRepository<T> where T : class, new()
    {
        Task<List<T>> Get();
        Task<T> Get(int id);
        Task<T> GetWithChilderen(int id, bool recursive);
        Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null);
        Task<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> GetWithChilderen(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllWithChilderen(bool recursive);
        AsyncTableQuery<T> AsQueryable();
        Task<int> Insert(T entity);
        void InsertWithChilderen(T entity, bool recursive);
        void InsertWithChilderenWithoutReplace(T entity, bool recursive);
        void InsertAllWithChilderen(List<T> entity, bool recursive);
        void InsertAllWithChilderenWithoutReplace(List<T> entities, bool recursive);
        Task<int> Update(T entity);
        void UpdateWithChilderen(T entity);
        Task<int> Delete(T entity);
        Task DeleteAll(List<T> list, bool recursive);
    }
}
