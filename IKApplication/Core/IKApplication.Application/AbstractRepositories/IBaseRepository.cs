using IKApplication.Domain.Entites;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Application.Repositories
{
    public interface IBaseRepository<T> where T : IBaseEntity  // sadece IBaseEntity ten kalıtım alanlar verilebilsin
    {
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);  // veritabanından silme işlemi yapmayız, status'ü pasife çekeriz !!
        Task<bool> Any(Expression<Func<T, bool>> expression);   // kayıt varsa true, yoksa false döner.  Mesela veritabanında kayıt arıyorsun, bu kayıt varmı yokmu diye bakacak
        Task<T> GetDefault(Expression<Func<T, bool>> expression);   // dinamik olarak where işlemi sağlar.  Id ye göre getir.  Id DB de yoksa null döner
        Task<List<T>> GetDefaults(Expression<Func<T, bool>> expression);   // Mesela  CompanyManagerID = 5 olan hepsini döndür. Bir liste dönecek sonuç olarak.



        //  Select , Where , Sıralama , Join
        // Hem select, hem order by yapabileceğimiz entitie 'leri birlikte çekmek için include etmek gerekir.  Bir sorguya birden fazla tablo girecek yani eagerloading kullanacağız
        Task<TResult> GetFilteredFirstOrDefault<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  // sıralama
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null//join   //using Microsoft.EntityFrameworkCore.Query;

            );   // orderBy verilmediyse null kullanacak.  include da girilmediyse null geçsin

        // GetFilteredFirstOrDefault metodunu kullandığımızda
        // 1.parametre olarak select edilmesini istediğimiz kolonlar,
        // 2.parametre olarak koşul,
        // 3. parametre olarak sıralama,
        // 4. parametre olarak  neyi join lemek istiyosak onu yazıyoruz

        // yukarıdakinin List olanı.  çoklu dönecek
        Task<List<TResult>> GetFilteredList<TResult>(
            Expression<Func<T, TResult>> select,
            Expression<Func<T, bool>> where,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,  // sıralama
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null  //join
            );

    }
}
