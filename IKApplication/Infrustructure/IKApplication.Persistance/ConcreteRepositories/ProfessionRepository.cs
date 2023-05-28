using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class ProfessionRepository : BaseRepository<Profession>, IProfessionRepository
    {
        public ProfessionRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {

        }
    }
}
