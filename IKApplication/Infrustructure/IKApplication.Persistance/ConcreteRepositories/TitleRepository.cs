using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class TitleRepository : BaseRepository<Title>, ITitleRepository
    {
        public TitleRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {

        }
    }
}
