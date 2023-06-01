using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class ReportRepository : BaseRepository<Report>, IReportRepository
    {
        public ReportRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {
        }
    }
}
