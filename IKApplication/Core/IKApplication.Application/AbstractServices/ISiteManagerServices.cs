using IKApplication.Application.DTOs.SiteManagerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Application.AbstractServices
{
    public interface ISiteManagerServices
    {
        Task Update(SiteManagerUpdateDTO siteManager);
        Task<SiteManagerUpdateDTO> GetById(int id);


    }
}
