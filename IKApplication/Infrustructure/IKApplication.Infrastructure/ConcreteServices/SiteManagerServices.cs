using AutoMapper;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.SiteManagerDTO;
using IKApplication.Domain.Entites;
using IKApplication.Persistance.ConcreteRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class SiteManagerServices : ISiteManagerServices
    {
        private readonly IMapper _mapper;
        private readonly AppUserRepository _appUserRepository;
        private SiteManagerServices(IMapper mapper, AppUserRepository appUserRepository) 
        {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public Task<SiteManagerUpdateDTO> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(SiteManagerUpdateDTO siteManager)
        {
            var manager = _mapper.Map<AppUser>(siteManager);
            if(manager.UploadPath != null) 
            {
                using var image = Image.Load(siteManager.UploadPath.OpenReadStream());
                image.Mutate(x => x.Resize(300, 300));
                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg");
                manager.ImagePath = $"/images/{guid}.jpg";
            }
            else
            {
                manager.ImagePath = siteManager.ImagePath;
            }
            await _appUserRepository.Update(manager);

        }

       
    }
}
