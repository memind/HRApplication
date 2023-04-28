using IKApplication.Application.Repositories;
using IKApplication.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Infrastructure.ConcreteRepositories
{
    public class CompanyManagerRepository : BaseRepository<CompanyManager>, ICompanyManagerRepository
    {
        public CompanyManagerRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {
            
        }
    }
}
