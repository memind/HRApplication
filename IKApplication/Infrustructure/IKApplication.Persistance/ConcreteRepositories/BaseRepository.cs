using IKApplication.Domain.Entites;
using System.Linq.Expressions;
using IKApplication.Application.AbstractRepositories;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;

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

               public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await table.AnyAsync(expression);
        }

        public async Task<T> GetDefault(Expression<Func<T, bool>> expression)
        {
            return await table.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return await table.Where(expression).ToListAsync();
        }

        public async Task<TResult> GetFilteredFirstOrDefault<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table; //Select * from Post
            if (where != null)
                query = query.Where(where); //Select*From where GenreID = 3
            if (orderBy != null)
                query = include(query);
            if (orderBy != null)
                return await orderBy(query).Select(select).FirstOrDefaultAsync();
            else
                return await query.Select(select).FirstOrDefaultAsync();
        }

        public async Task<List<TResult>> GetFilteredList<TResult>(Expression<Func<T, TResult>> select, Expression<Func<T, bool>> where, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = table; //Select * from Post
            if (where != null)
                query = query.Where(where); //Select*From where GenreID = 3
            if (include != null)
                query = include(query);
            if (orderBy != null)
                return await orderBy(query).Select(select).ToListAsync();
            else
                return await query.Select(select).ToListAsync();
        }
    }
}