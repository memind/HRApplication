using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ISectorRepository _sectorRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        //Dependency Injection
        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, ICompanyRepository companyRepository, ISectorRepository sectorRepository)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _companyRepository = companyRepository;
            _sectorRepository = sectorRepository;
        }
        //USerName ile AppUser tablosunda bulunan (eğer varsa) AppUser satrını çekeriz ve UpdateProfileDTO nesnesini doldururuz.
        public async Task<AppUserUpdateDTO> GetByUserName(string userName)
        {
            AppUserUpdateDTO result = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserUpdateDTO
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
                    Email = x.Email,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    CompanyId = x.CompanyId
                },
                where: x => x.UserName == userName);

            result.Companies = await _companyRepository.GetFilteredList(
                select: x => new CompanyVM
                {
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    SectorName = x.Sector.Name,
                    NumberOfEmployees = x.NumberOfEmployees
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name),
                include: x => x.Include(x => x.Sector));

            return result;
        }
        public async Task<bool> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, true);
                return true;
            }

            return false;
        }

        public async Task<IdentityResult> CreateUser(AppUserCreateDTO model, string role)
        {
            var appUser = _mapper.Map<AppUser>(model);
            appUser.UserName = model.Email;
            var result = await _userManager.CreateAsync(appUser, string.IsNullOrEmpty(model.Password) ? "" : model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, role);
                return result;
            }

            return result;
        }

        ////sistemden çıkıç için kullanırız. User bilgileri sessiondan silinşr.
        //public async Task LogOut()
        //{
        //    await _signInManager.SignOutAsync();
        //}

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
        public async Task UpdateUser(AppUserUpdateDTO model)
        {
            //Update işlemlerind eönce Id ile ilgili nesneyi rame çekeriz. Dışarıdan gelen güncel bilgilerle değişiklikleri yaparız.
            //En Son SaveChanges ile veri tabanına güncellemeleri göndeririz. 

            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == model.Id);

            if (appUser != null && model.Email == appUser.Email)
            {
                appUser = _mapper.Map<AppUser>(model);
                if (model.Password != null)
                    appUser.PasswordHash = _userManager.PasswordHasher.HashPassword(appUser, model.Password);
            }
        }

        public async Task<AppUserUpdateDTO> GetById(Guid id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);

            if (appUser == null)
            {
                var model = _mapper.Map<AppUserUpdateDTO>(appUser);

                model.Companies = await _companyRepository.GetFilteredList(
                    select: x => new CompanyVM
                    {
                        Name = x.Name,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        SectorName = x.Sector.Name,
                        NumberOfEmployees = x.NumberOfEmployees
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderBy(x => x.Name),
                    include: x => x.Include(x => x.Sector));

                return model;
            }

            return null;
        }

        public async Task<List<Sector>> GetSectorsAsync()
        {
            return await _sectorRepository.GetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified);
        }

        public async Task RegisterUserWithCompany(RegisterVM registerVm, string role)
        {
            Company newCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Name = registerVm.CompanyName,
                Email = registerVm.CompanyEmail,
                PhoneNumber = registerVm.CompanyPhoneNumber,
                SectorId = registerVm.CompanySectorId,
                NumberOfEmployees = registerVm.CompanyNumberOfEmployees,
                CreateDate = registerVm.CompanyCreateDate,
                Status = registerVm.CompanyStatus
            };

            AppUser newUser = new AppUser()
            {
                Name = registerVm.UserName,
                SecondName = registerVm.UserSecondName,
                Surname = registerVm.UserSurname,
                Title = registerVm.UserTitle,
                BloodGroup = registerVm.UserBloodGroup,
                Profession = registerVm.UserProfession,
                BirthDate = registerVm.UserBirthDate,
                IdentityId = registerVm.UserIdentityId,
                Email = registerVm.UserEmail,
                ImagePath = registerVm.UserImagePath,
                CreateDate = registerVm.UserCreateDate,
                Status = registerVm.UserStatus,
                CompanyId = newCompany.Id,
                Company = newCompany
            };

            newUser.UserName = registerVm.UserEmail;

            if (registerVm.UserPassword == registerVm.UserConfirmPassword)
            {
                await _companyRepository.Create(newCompany);
                var result = await _userManager.CreateAsync(newUser, string.IsNullOrEmpty(registerVm.UserPassword) ? "" : registerVm.UserPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, role);
                }
            }

        }
    }
}
