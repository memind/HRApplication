using IKApplication.Domain.Entites;

namespace IKApplication.Application.AbstractRepositories
{
    internal interface IAppUserRepository : IBaseRepository<AppUser>
    {
        //AppUser ile ilgili özel üyeler varsa buraya eklenecek.
    }
}
