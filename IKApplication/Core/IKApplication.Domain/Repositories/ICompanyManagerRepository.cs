using IKApplication.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Domain.Repositories
{
    internal interface ICompanyManagerRepository : IBaseRepository<CompanyManager>
    {
        //CompanyManager ile ilgili özel üyeler varsa buraya eklenecek.
    }
}
