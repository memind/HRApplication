using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {

        }
    }
}
