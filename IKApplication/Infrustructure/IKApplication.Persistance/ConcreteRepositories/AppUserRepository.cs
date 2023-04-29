using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class AppUserRepository : BaseRepository<AppUser>
    {
        public AppUserRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {
            
        }
    }
}
