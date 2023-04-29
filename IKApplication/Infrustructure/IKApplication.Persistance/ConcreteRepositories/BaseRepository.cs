using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        public Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task Create(T entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            throw new NotImplementedException();
        }

        public Task Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
