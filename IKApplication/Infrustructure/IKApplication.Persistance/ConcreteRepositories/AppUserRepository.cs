using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class AppUserRepository : BaseRepository<AppUser>,IAppUserRepository
    {
        public AppUserRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {
            
        }
    }
}
