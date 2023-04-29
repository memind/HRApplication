using IKApplication.Domain.Entites;

namespace IKApplication.Application.AbstractRepositories
{
    public interface IAppUserRepository : IBaseRepository<AppUser>
    {
        //AppUser ile ilgili özel üyeler varsa buraya eklenecek.
    }
}
