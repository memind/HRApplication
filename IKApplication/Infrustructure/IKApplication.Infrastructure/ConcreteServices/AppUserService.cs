using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.SectorVMs;
using IKApplication.Application.VMs.TitleVMs;
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
        private readonly ITitleRepository _titleRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        //Dependency Injection
        public AppUserService(IAppUserRepository appUserRepository, ICompanyRepository companyRepository, ISectorRepository sectorRepository, ITitleRepository titleRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper)
        {
            _appUserRepository = appUserRepository;
            _companyRepository = companyRepository;
            _sectorRepository = sectorRepository;
            _titleRepository = titleRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        //USerName ile AppUser tablosunda bulunan (eğer varsa) AppUser satrını çekeriz ve UpdateProfileDTO nesnesini doldururuz.
        public async Task<AppUserUpdateDTO> GetByUserName(string userName)
        {
            AppUserUpdateDTO model = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserUpdateDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    Email = x.Email,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                },
                where: x => x.UserName == userName);

            if (model != null)
            {
                model.Companies = await _companyRepository.GetFilteredList(
                        select: x => new CompanyVM
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Email = x.Email,
                            PhoneNumber = x.PhoneNumber,
                            SectorName = x.Sector.Name,
                            NumberOfEmployees = x.NumberOfEmployees,
                        },
                        where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                        orderBy: x => x.OrderBy(x => x.CreateDate),
                        include: x => x.Include(x => x.Sector));

                model.Titles = await _titleRepository.GetFilteredList(
                        select: x => new TitleVM
                        {
                            Id = x.Id,
                            Name = x.Name
                        },
                        where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                        orderBy: x => x.OrderBy(x => x.CreateDate));

                return model;
            }

            return null;
        }

        public async Task<AppUserUpdateDTO> GetById(Guid id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);

            if (appUser != null)
            {
                var model = _mapper.Map<AppUserUpdateDTO>(appUser);

                if (model != null)
                {
                    model.Companies = await _companyRepository.GetFilteredList(
                            select: x => new CompanyVM
                            {
                                Id = x.Id,
                                Name = x.Name,
                                Email = x.Email,
                                PhoneNumber = x.PhoneNumber,
                                SectorName = x.Sector.Name,
                                NumberOfEmployees = x.NumberOfEmployees,
                            },
                            where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                            orderBy: x => x.OrderBy(x => x.CreateDate),
                            include: x => x.Include(x => x.Sector));

                    model.Titles = await _titleRepository.GetFilteredList(
                            select: x => new TitleVM
                            {
                                Id = x.Id,
                                Name = x.Name
                            },
                            where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                            orderBy: x => x.OrderBy(x => x.CreateDate));

                    return model;
                }

                return null;
            }

            return null;
        }

        public async Task<List<AppUserVM>> GetAllUsers()
        {
            var users = await _appUserRepository.GetFilteredList(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    UserName = x.UserName,
                    Email = x.Email,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    CompanyName = x.Company.Name,
                    Title = x.Title.Name,
                },
                where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                include: x => x.Include(x => x.Company).Include(x => x.Title));

            foreach (var user in users)
            {
                user.Roles = (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(user.UserName));
            }

            return users;
        }

        public async Task<AppUserVM> GetCurrentUserInfo(string userName)
        {
            AppUserVM model = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    UserName = x.UserName,
                    Email = x.Email,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    CompanyName = x.Company.Name,
                    Title = x.Title.Name,
                },
                where: x => x.UserName == userName,
                include: x => x.Include(x => x.Company).Include(x => x.Title));

            model.Roles = (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(userName));

            return model;
        }

        public async Task<List<RegisterVM>> GetAllRegistrations()
        {
            var registers = await _appUserRepository.GetFilteredList(
                select: x => new RegisterVM
                {
                    UserId = x.Id,
                    UserName = x.Name,
                    UserSecondName = x.SecondName,
                    UserSurname = x.Surname,
                    UserTitle = x.Title.Name,
                    UserEmail = x.Email,
                    CompanyId = x.Company.Id,
                    CompanyName = x.Company.Name,
                    CompanySector = x.Company.Sector.Name,
                    NumberOfEmployees = x.Company.NumberOfEmployees
                },
                where: x => (x.Status == Status.Passive),
                include: x => x.Include(x => x.Company).Include(x => x.Title));

            return registers;
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

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> CreateUser(AppUserCreateDTO model, string role)
        {
            var appUser = _mapper.Map<AppUser>(model);
            //!!!!!!!!!!Email'in daha önce kullanılıp kullanılmadığını kontrol et!!!!!!!!!!!!
            appUser.UserName = model.Email;
            var result = await _userManager.CreateAsync(appUser, string.IsNullOrEmpty(model.Password) ? "" : model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, role);
                return result;
            }

            return result;
        }

        public async Task UpdateUser(AppUserUpdateDTO model)
        {
            //Update işlemlerind eönce Id ile ilgili nesneyi rame çekeriz. Dışarıdan gelen güncel bilgilerle değişiklikleri yaparız.
            //En Son SaveChanges ile veri tabanına güncellemeleri göndeririz. 

            AppUser user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (user != null)
            {
                if (model.Password != null)
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                }

                user.Name = model.Name;
                user.SecondName = model.SecondName;
                user.Surname = model.Surname;
                user.BloodGroup = model.BloodGroup;
                user.Profession = model.Profession;
                user.BirthDate = model.BirthDate;
                user.IdentityNumber = model.IdentityNumber;
                user.ImagePath = model.ImagePath;
                user.CompanyId = model.CompanyId;
                user.TitleId = model.TitleId;
                user.CreateDate = model.CreateDate;
                user.UpdateDate = model.UpdateDate;
                user.Status = model.Status;

                await _userManager.UpdateAsync(user);
            }
        }

        public async Task Delete(Guid id)
        {
            AppUser user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
            {
                user.DeleteDate = DateTime.Now;
                user.Status = Status.Deleted;

                await _appUserRepository.Delete(user);
            }
        }
        
        public async Task<RegisterDTO> CreateRegister()
        {
            RegisterDTO model = new RegisterDTO();

            model.Titles = await _titleRepository.GetFilteredList(
                    select: x => new TitleVM
                    {
                        Id = x.Id,
                        Name = x.Name
                    },
                    where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                    orderBy: x => x.OrderBy(x => x.CreateDate));

            model.Sectors = await _sectorRepository.GetFilteredList(
                    select: x => new SectorVM
                    {
                        Id = x.Id,
                        Name = x.Name,
                        CompanyCount = x.Companies.Count,
                    },
                    where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.Companies));

            return model;
        }

        public async Task RegisterUserWithCompany(RegisterDTO register, string role)
        {
            Company newCompany = new Company()
            {
                Id = Guid.NewGuid(),
                Name = register.CompanyName,
                Email = register.CompanyEmail,
                PhoneNumber = register.CompanyPhoneNumber,
                NumberOfEmployees = register.CompanyNumberOfEmployees,
                CreateDate = register.CompanyCreateDate,
                Status = register.CompanyStatus,
                SectorId = register.CompanySectorId
            };

            AppUser newUser = new AppUser()
            {
                Name = register.UserName,
                SecondName = register.UserSecondName,
                Surname = register.UserSurname,
                BloodGroup = register.UserBloodGroup,
                Profession = register.UserProfession,
                BirthDate = register.UserBirthDate,
                IdentityNumber = register.UserIdentityNumber,
                ImagePath = register.UserImagePath,
                CreateDate = register.UserCreateDate,
                Status = register.UserStatus,
                Email = register.UserEmail,
                CompanyId = newCompany.Id,
                TitleId = register.UserTitleId,
            };

            newUser.UserName = register.UserEmail;

            if (register.UserPassword == register.UserConfirmPassword)
            {
                var result = await _userManager.CreateAsync(newUser, string.IsNullOrEmpty(register.UserPassword) ? "" : register.UserPassword);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, role);
                    await _companyRepository.Create(newCompany);
                }
            }
        }
    }
}
