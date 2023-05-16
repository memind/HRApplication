using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class CashAdvanceRepository : BaseRepository<CashAdvance>, ICashAdvanceRepository
    {
        public CashAdvanceRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {

        }
    }
}
