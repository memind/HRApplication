using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using IKApplication.Application.AbstractRepositories;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        // DEPENDENCY INJECTION
        private readonly IKAppDbContext _iKAppDbContext;
        protected Microsoft.EntityFrameworkCore.DbSet<T> table;
        //private DbSet<T> _table { get => _iKAppDbContext.Set<T>(); }
        public BaseRepository(IKAppDbContext iKAppDbContext)
        {
            _iKAppDbContext = iKAppDbContext;
            table = _iKAppDbContext.Set<T>();
        }

        // C R U D
        // C R U D
        // C R U D

        // C R E A T E
        public async Task Create(T entity)
        {
            table.Add(entity);
            await _iKAppDbContext.SaveChangesAsync();
        }

        //  U P D A T E
        public async Task Update(T entity)
        {
            _iKAppDbContext.Entry<T>(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _iKAppDbContext.SaveChangesAsync();
        }

        // D E L E T E
        public async Task Delete(T entity)
        {
            await _iKAppDbContext.SaveChangesAsync();
            // to do: servis katmanında entity'sine göre pasif hale getireceğiz.    DB den silmeyeceğizde pasif hale çekeceğiz, kullanıcı silindi zannedecek
        }


        //-------------------------------------
        //-------------------------------------
        //-------------------------------------

        public Task<bool> Any(Expression<Func<T, bool>> expression)
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
    }
}
