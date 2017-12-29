using Fit.DataModels;
using Fit.DataStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;
using SQLite;
using System.Collections;

namespace Fit.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private Database database;

        public Repository()
        {
            this.database = App.database;         
        }

        public AsyncTableQuery<T> AsQueryable() =>
            database.GetDatabase().Table<T>();
        

        public async Task<List<T>> Get() =>
            await database.GetDatabase().Table<T>().ToListAsync();


        public async Task<List<T>> Get<TValue>(Expression<Func<T, bool>> predicate = null, Expression<Func<T, TValue>> orderBy = null)
        {
            using (await database.GetLock().LockAsync())
            {
                var query = database.GetDatabase().Table<T>();

                if (predicate != null)
                    query = query.Where(predicate);

                if (orderBy != null)
                    query = query.OrderBy<TValue>(orderBy);

                return await query.ToListAsync();
            }
        }

        public async Task<T> Get(int id)
        {
            using (await database.GetLock().LockAsync())
            {
               return await database.GetDatabase().FindAsync<T>(id);
            }
        }

        public async Task<T> GetWithChilderen(int id, bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                return await database.GetDatabase().FindWithChildrenAsync<T>(id, recursive);
            }
        }

        public async Task<List<T>> GetAllWithChilderen(bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                return await database.GetDatabase().GetAllWithChildrenAsync<T>(null, recursive);
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            using (await database.GetLock().LockAsync())
            {
               
               return await database.GetDatabase().FindAsync<T>(predicate);
            }
        }

        public async Task<T> GetWithChilderen(Expression<Func<T, bool>> predicate)
        {
            using (await database.GetLock().LockAsync())
            {

                return await database.GetDatabase().FindWithChildrenAsync<T>(predicate);
            }
        }



        public async Task<int> Insert(T entity)
        {
            using (await database.GetLock().LockAsync())
            {
               return await database.GetDatabase().InsertAsync(entity);
            }
        }

        public async void InsertWithChilderen(T entity, bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                await database.GetDatabase().InsertOrReplaceWithChildrenAsync(entity, recursive);
            }
        }

        public async void InsertWithChilderenWithoutReplace(T entity, bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                await database.GetDatabase().InsertWithChildrenAsync(entity, recursive);
            }
        }


        public async void InsertAllWithChilderen(List<T> entities, bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                await database.GetDatabase().InsertOrReplaceAllWithChildrenAsync(entities, recursive);
            }
        }

        public async void InsertAllWithChilderenWithoutReplace(List<T> entities, bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                await database.GetDatabase().InsertAllWithChildrenAsync(entities, recursive);
            }
        }

        public async Task<int> Update(T entity)
        {
            using (await database.GetLock().LockAsync())
            {
               return await database.GetDatabase().UpdateAsync(entity);
            }
        }

        public async void UpdateWithChilderen(T entity)
        {
            using (await database.GetLock().LockAsync())
            {
                 await database.GetDatabase().UpdateWithChildrenAsync(entity);
            }
        }

        public async Task<int> Delete(T entity)
        {
            using (await database.GetLock().LockAsync())
            {
           
                return await database.GetDatabase().DeleteAsync(entity);
            }
        }

        public async Task DeleteAll(List<T> list, bool recursive)
        {
            using (await database.GetLock().LockAsync())
            {
                 await database.GetDatabase().DeleteAllAsync(list, recursive);
                
            }
        }




    }
}

