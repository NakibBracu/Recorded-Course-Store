using NHibernate;
using NHibernate.Linq;
using RCS.Data.Entities;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace RCS.Data.Repositories
{
    public class Repository<T, TKey> : IRepositoryBase<T, TKey> where T : class, IEntity<TKey>
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public async Task AddAsync(T entity)
        {
            await _session.SaveAsync(entity);
            await _session.FlushAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _session.Update(entity);
            await _session.FlushAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _session.Delete(entity);
            await _session.FlushAsync();
        }

        public async Task AddOrUpdateAsync(T entity)
        {
            _session.SaveOrUpdate(entity);
            await _session.FlushAsync();
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>>? predicate = null!)
        {
            var query = _session.QueryOver<T>();

            if (predicate != null)
                query = query.Where(predicate);

            return await query.RowCountAsync();
        }

        public async Task<T?> GetSingleAsync(TKey id)
        {
            return await _session.GetAsync<T>(id);
        }

        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _session.Query<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _session.Query<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>>? predicate = null!)
        {
            return await _session.Query<T>().Where(predicate ?? (x => true)).ToListAsync();
        }

        public async Task<(IList<T> data, int total, int totalDisplay)> GetByPagingAsync(
       Expression<Func<T, bool>> filter = null!, string orderBy = null!, int pageIndex = 1,
       int pageSize = 10, Expression<Func<T, object>>? objectSelector = null!,
       Expression<Func<T, bool>> selectorFilter = null!)
       {
            var query = _session.Query<T>().Where(filter ?? (x => true));

            if (selectorFilter != null)
            {
                query = query.Where(selectorFilter);
            }

            var total = await query.CountAsync();

            //if (!string.IsNullOrEmpty(orderBy))
            //{
            //    // Convert string property name to a lambda expression
            //    var orderExpression = Expression.Lambda<Func<T, object>>(
            //        Expression.Convert(Expression.PropertyOrField(Expression.Parameter(typeof(T)), orderBy), typeof(object)),
            //        Expression.Parameter(typeof(T))
            //    );

            //    query = query.OrderBy(orderExpression);
            //}

            //if (objectSelector != null)
            //{
            //    query = (IQueryable<T>)query.Select(objectSelector);
            //}

            var data = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return (data, total, data.Count);
        }



    }
}
