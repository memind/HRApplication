using IKApplication.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Application.Repositories
{
    public interface ICompanyManagerRepository : IBaseRepository<CompanyManager>
    {
        //CompanyManager ile ilgili özel üyeler varsa buraya eklenecek.
    }
}
