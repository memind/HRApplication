using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Persistance.ConcreteRepositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext) { }
    }
}
