using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.SiteManagerDTO;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Domain.Entites;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class AppUserServices :IAppUserServices
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        //Dependency Injection
        public AppUserServices(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }
        //USerName ile AppUser tablosunda bulunan (eğer varsa) AppUser satrını çekeriz ve UpdateProfileDTO nesnesini doldururuz.
        public async Task<SiteManagerUpdateDTO> GetByUserName(string userName)
        {
            SiteManagerUpdateDTO  result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new SiteManagerUpdateDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    Title = x.Title,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityId = x.IdentityId,
                    ImagePath = x.ImagePath

                },
                where: x => x.UserName == userName
              );
            return result;

        }

        //Kullanıcının sisteme Login olmasını sağlar.User bilgilerine ulaşabilirz.
        public async Task<SignInResult> Login(LoginDTO model)
        {
            return await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
        }

        //sistemden çıkıç için kullanırız. User bilgileri sessiondan silinşr.
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        //Yeni bir AppUSer oluştururuz.
        //public async Task<IdentityResult> Register(RegisterDTO model)
        //{
        //    //Gelen RegisterDTO,Create edilmesi gereken AppUser
        //    //AutoMapper kullanacağız.

        //    AppUser user = new AppUser();
        //    user.UserName = model.UserName;
        //    user.Email = model.Email;
        //    //user.PasswordHash = model.PasswordHash;
        //    user.CreatedDate = model.CreateDate;

        //    IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        await _signInManager.SignInAsync(user, isPersistent: false);
        //    }
        //    return result;
        //}

        //Kullanıcı bilgilerini güncellemek için kullanırız.
        //Kullanıcının güncellemek istediği  bilgileri View'dan UpdateProfileDTO nesnesi aracılığıyla alırız. REsim, Email, Password alanlarını kontrol ederekgüncelleriz.
        public async Task UpdateUser(AppUser model)
        {
            //Update işlemlerind eönce Id ile ilgili nesneyi rame çekeriz. Dışarıdan gelen güncel bilgilerle değişiklikleri yaparız.
            //En Son SaveChanges ile veri tabanına güncellemeleri göndeririz. 

            //Todo Mapper
            var user2 = _mapper.Map<AppUser>(model);

            AppUser user = await _appUserRepository.GetDefault(x => x.Id == user2.Id);
            //Todo Upload Resim işlemleri 
            if (model.UploadPath != null)
            {
                using var image = Image.Load(model.UploadPath.OpenReadStream());

                //REsize
                image.Mutate(x => x.Resize(300, 300));

                Guid guid = Guid.NewGuid();
                image.Save($"wwwroot/images/{guid}.jpg"); //folder ın altına kaydettik.


                user.ImagePath = $"wwwroot/images/{guid}.jpg";
            }
            else
            {
                if (model.UploadPath != null)
                {
                    user.ImagePath = model.ImagePath;
                }
                else
                    user.ImagePath = $"/images/defaultuser.jpg";
            }

            user.Status = Domain.Enums.Status.Modified;
            user.UpdateDate = DateTime.Now;
            await _appUserRepository.Create(user);


            //if (model.Password != null)
            //{
            //    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
            //    await _userManager.UpdateAsync(user);

            //}
            if (model.Email != null)
            {
                AppUser İsUserEmailExist = await _userManager.FindByEmailAsync(model.Email);
                if (İsUserEmailExist != null)
                {
                    await _userManager.SetEmailAsync(user, model.Email);
                }
            }
        }

        public async Task<AppUser> GetById(Guid id)
        {
            AppUser user = await _appUserRepository.GetDefault(x => x.Id == id);
            var model = _mapper.Map<AppUser>(user);
            var appUser = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUser()
                {
                    Id = x.Id,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    Title = x.Title,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityId = x.IdentityId,
                    ImagePath = x.ImagePath

                },
                where: null,
                orderBy: null,
                include: null
                );
            return appUser;
        }
    }
}
