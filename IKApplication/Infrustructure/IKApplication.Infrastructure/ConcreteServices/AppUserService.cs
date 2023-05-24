using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.DTOs.TitleDTOs;
using IKApplication.Application.DTOs.UserDTOs;
using IKApplication.Application.VMs.UserVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ICompanyService _companyService;
        private readonly ITitleService _titleService;
        private readonly ISectorService _sectorService;

        //Dependency Injection
        public AppUserService(IAppUserRepository appUserRepository, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper, ICompanyService companyService, ITitleService titleService, ISectorService sectorService)
        {
            _appUserRepository = appUserRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _companyService = companyService;
            _titleService = titleService;
            _sectorService = sectorService;
        }

        //USerName ile AppUser tablosunda bulunan (eğer varsa) AppUser satrını çekeriz ve UpdateProfileDTO nesnesini doldururuz.
        public async Task<AppUserUpdateDTO> GetByUserName(string userName)
        {
            AppUserUpdateDTO model = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserUpdateDTO
                {
                    Id = x.Id,
                    Email = x.Email,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    CreateDate = x.CreateDate,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    JobStartDate = x.JobStartDate,
                    PhoneNumber = x.PhoneNumber
                },
                where: x => x.UserName == userName);

            if (model != null)
            {
                model.Companies = await _companyService.GetAllCompanies();

                model.Titles = await _titleService.GetAllTitles();
            }
            return model;
        }

        public async Task<AppUserUpdateDTO> GetById(Guid id)
        {
            AppUser appUser = await _appUserRepository.GetDefault(x => x.Id == id);

            if (appUser != null)
            {
                var model = _mapper.Map<AppUserUpdateDTO>(appUser);

                if (model != null)
                {
                    model.Companies = await _companyService.GetAllCompanies();

                    model.Titles = await _titleService.GetAllTitles();
                }
                return model;
            }
            return null;
        }

        public async Task<List<AppUserVM>> GetAllUsers()
        {
            var users = await _appUserRepository.GetFilteredList(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    CompanyName = x.Company.Name,
                    Title = x.Title,
                    JobStartDate = x.JobStartDate,
                    PhoneNumber = x.PhoneNumber,
                    PatronId = x.PatronId
                },
                where: x => (x.Status == Status.Active || x.Status == Status.Modified),
                include: x => x.Include(x => x.Company).Include(x => x.Title).Include(x => x.Patron));

            foreach (var user in users)
            {
                user.Roles = (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(user.UserName));
            }

            return users;
        }

        public async Task<List<AppUserVM>> GetUsersByCompany(Guid companyId)
        {
            var users = await _appUserRepository.GetFilteredList(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    CompanyName = x.Company.Name,
                    Title = x.Title,
                    JobStartDate = x.JobStartDate,
                    PhoneNumber = x.PhoneNumber,
                    PatronId = x.PatronId
                },
                where: x => (x.Status == Status.Active || x.Status == Status.Modified) && (x.CompanyId == companyId),
                include: x => x.Include(x => x.Company).Include(x => x.Title).Include(x => x.Patron));

            foreach (var user in users)
            {
                user.Roles = (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(user.UserName));
            }

            return users;
        }

        public async Task<List<AppUserVM>> GetUsersByRole(string role)
        {
            List<AppUserVM> model = new List<AppUserVM>();

            var users = await _userManager.GetUsersInRoleAsync(role);

            foreach (var user in users)
            {
                if (user.Status == Status.Active || user.Status == Status.Modified)
                {
                    AppUserVM activeUser = _mapper.Map<AppUserVM>(user);
                    model.Add(activeUser);
                }
            }
            return model;
        }

        public async Task<AppUserVM> GetCurrentUserInfo(string userName)
        {
            AppUserVM model = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    CompanyName = x.Company.Name,
                    Title = x.Title,
                    JobStartDate = x.JobStartDate,
                    PhoneNumber = x.PhoneNumber,
                    PatronId = x.PatronId,
                    Patron = x.Patron
                },
                where: x => x.UserName == userName,
                include: x => x.Include(x => x.Company).Include(x => x.Title).Include(x => x.Patron));

            model.Roles = (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByNameAsync(userName));

            return model;
        }
        public async Task<AppUserVM> GetCurrentUserInfo(Guid id)
        {
            AppUserVM model = await _appUserRepository.GetFilteredFirstOrDefault(
                select: x => new AppUserVM
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Email = x.Email,
                    Name = x.Name,
                    SecondName = x.SecondName,
                    Surname = x.Surname,
                    BloodGroup = x.BloodGroup,
                    Profession = x.Profession,
                    BirthDate = x.BirthDate,
                    IdentityNumber = x.IdentityNumber,
                    ImagePath = x.ImagePath,
                    CompanyId = x.CompanyId,
                    TitleId = x.TitleId,
                    CompanyName = x.Company.Name,
                    Title = x.Title,
                    JobStartDate = x.JobStartDate,
                    PhoneNumber = x.PhoneNumber,
                    PatronId = x.PatronId,
                    Patron = x.Patron
                },
                where: x => x.Id == id,
                include: x => x.Include(x => x.Company).Include(x => x.Title).Include(x => x.Patron));

            model.Roles = (List<string>)await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model.Email));

            return model;
        }

        public async Task<List<RegisterVM>> GetAllRegistrations()
        {
            return await _appUserRepository.GetFilteredList(
                select: x => new RegisterVM
                {
                    UserId = x.Id,
                    UserEmail = x.Email,
                    UserName = x.Name,
                    UserSecondName = x.SecondName,
                    UserSurname = x.Surname,
                    UserTitle = x.Title.Name,
                    CompanyId = x.Company.Id,
                    CompanyName = x.Company.Name,
                    CompanySector = x.Company.Sector.Name,
                    NumberOfEmployees = x.Company.NumberOfEmployees
                },
                where: x => (x.Status == Status.Passive),
                include: x => x.Include(x => x.Company).Include(x => x.Title));
        }

        public async Task<bool> Login(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                await _signInManager.SignInAsync(user, false);
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
            var user = await _userManager.FindByNameAsync(model.Email);

            if (user == null && model.Password != null && model.Password == model.ConfirmPassword)
            {
                var appUser = _mapper.Map<AppUser>(model);
                appUser.UserName = model.Email;
                appUser.Id = Guid.NewGuid();

                var result = await _userManager.CreateAsync(appUser, string.IsNullOrEmpty(model.Password) ? "" : model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, role);
                }
                return result;
            }
            return IdentityResult.Failed(new IdentityError() { Code = "Create Error", Description = "User could not created. Check your information." });
        }

        public async Task UpdateUser(AppUserUpdateDTO model)
        {
            AppUser user = await _userManager.FindByNameAsync(model.Email);

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
                user.JobStartDate = model.JobStartDate;
                user.IdentityNumber = model.IdentityNumber;
                user.ImagePath = model.ImagePath;
                user.TitleId = model.TitleId;
                user.CreateDate = model.CreateDate;
                user.PhoneNumber = model.PhoneNumber;
                user.UpdateDate = model.UpdateDate;
                user.Status = model.Status;
                user.PatronId = model.PatronId;

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

            model.Sectors = await _sectorService.GetAllSectors();

            return model;
        }

        public async Task RegisterUserWithCompany(RegisterDTO register, string role)
        {
            CompanyCreateDTO company = new CompanyCreateDTO()
            {
                Id = Guid.NewGuid(),
                Name = register.CompanyName,
                Email = register.CompanyEmail,
                PhoneNumber = register.CompanyPhoneNumber,
                NumberOfEmployees = register.CompanyNumberOfEmployees,
                SectorId = register.CompanySectorId
            };

            await _companyService.Create(company);

            TitleCreateDTO title = new TitleCreateDTO()
            {
                Id = Guid.NewGuid(),
                Name = register.UserTitle,
                CompanyId = company.Id
            };

            await _titleService.Create(title);

            AppUserCreateDTO user = new AppUserCreateDTO()
            {
                Name = register.UserName,
                SecondName = register.UserSecondName,
                Surname = register.UserSurname,
                BloodGroup = register.UserBloodGroup,
                Profession = register.UserProfession,
                BirthDate = register.UserBirthDate,
                IdentityNumber = register.UserIdentityNumber,
                Email = register.UserEmail,
                PhoneNumber = register.UserPhoneNumber,
                Password = register.UserPassword,
                ConfirmPassword = register.UserConfirmPassword,
                ImagePath = register.UserImagePath,
                CompanyId = company.Id,
                TitleId = title.Id
            };
            await CreateUser(user, role);
        }

        public async Task AddCompanyManager(AppUserCreateDTO user, CompanyUpdateDTO company)
        {
            var companyMap = _mapper.Map<Company>(company);
            var userMap = _mapper.Map<AppUser>(user);
            companyMap.CompanyManagers.Add(userMap);

            var companyUpdate = _mapper.Map<CompanyUpdateDTO>(companyMap);

            await _companyService.Update(companyUpdate);
        }

        public async Task<Guid> GetUserId(string userName)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            return user.Id;
        }
    }
}
