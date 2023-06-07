using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CashAdvanceService : ICashAdvanceService
    {
        private readonly ICashAdvanceRepository _cashAdvanceRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;

        public CashAdvanceService(ICashAdvanceRepository cashAdvanceRepository, IMapper mapper, IAppUserService appUserService)
        {
            _cashAdvanceRepository = cashAdvanceRepository;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public async Task Create(CashAdvanceCreateDTO createCashAdvanceDTO)
        {
            var map = _mapper.Map<CashAdvance>(createCashAdvanceDTO);
            var advanceTo = await _appUserService.GetCurrentUserInfo((Guid)createCashAdvanceDTO.AdvanceToId);

            map.AdvanceToId = advanceTo.Id;
            map.DirectorId = (Guid)advanceTo.PatronId;
            map.CompanyId = (Guid)advanceTo.CompanyId;

            await _cashAdvanceRepository.Create(map);
        }

        public async Task Update(CashAdvanceUpdateDTO updateCashAdvanceDTO)
        {
            var advance = await _cashAdvanceRepository.GetDefault(x => x.Id == updateCashAdvanceDTO.Id);
            if (advance != null)
            {
                advance.Status = Status.Passive;
                advance.Currency = updateCashAdvanceDTO.Currency;
                advance.Description = updateCashAdvanceDTO.Description;
                advance.RequestedAmount = updateCashAdvanceDTO.RequestedAmount;
                advance.FinalDateRequest = updateCashAdvanceDTO.FinalDateRequest;
                await _cashAdvanceRepository.Update(advance);
            }
        }

        public async Task Delete(Guid id)
        {
            var advance = await _cashAdvanceRepository.GetDefault(x => x.Id == id);
            advance.DeleteDate = DateTime.Now;
            advance.Status = Status.Deleted;
            await _cashAdvanceRepository.Delete(advance);
        }

        public async Task<List<CashAdvanceVM>> GetAdvanceRequests(Guid companyId)
        {
            var companyExpenses = await _cashAdvanceRepository.GetFilteredList
                (
                    select: x => new CashAdvanceVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        Description = x.Description,
                        RequestedAmount = x.RequestedAmount,
                        Director = x.Director,
                        AdvanceTo = x.AdvanceTo,
                        IsPaymentProcessed = x.IsPaymentProcessed,
                        FinalDateRequest = x.FinalDateRequest,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        DeleteDate = x.DeleteDate,
                        CompanyId = x.CompanyId,
                        InstallmentCount = x.InstallmentCount,
                        AdvanceToId = x.AdvanceToId,
                        DirectorId = x.DirectorId,
                        Currency = x.Currency
                    },
                    where: x => x.Status == Status.Passive && x.AdvanceTo.CompanyId == companyId && x.DirectorId == x.AdvanceTo.PatronId,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AdvanceTo).Include(x => x.Director)
                );

            return companyExpenses;
        }

        public async Task<List<CashAdvanceVM>> GetAllAdvances(Guid companyId)
        {
            var companyAdvances = await _cashAdvanceRepository.GetFilteredList
                (
                    select: x => new CashAdvanceVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        Description = x.Description,
                        RequestedAmount = x.RequestedAmount,
                        Director = x.Director,
                        AdvanceTo = x.AdvanceTo,
                        IsPaymentProcessed = x.IsPaymentProcessed,
                        FinalDateRequest = x.FinalDateRequest,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        InstallmentCount = x.InstallmentCount,
                        DeleteDate = x.DeleteDate,
                        CompanyId = x.CompanyId,
                        AdvanceToId = x.AdvanceToId,
                        DirectorId = x.DirectorId,
                        Currency = x.Currency
                    },
                    where: x => x.CompanyId == companyId && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.Director).Include(x => x.AdvanceTo)
                );

            return companyAdvances;
        }

        public async Task<CashAdvanceUpdateDTO> GetById(Guid id)
        {
            var advance = await _cashAdvanceRepository.GetFilteredFirstOrDefault
                (
                    select: x => new CashAdvanceUpdateDTO()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        RequestedAmount = x.RequestedAmount,
                        FinalDateRequest = (DateTime)x.FinalDateRequest,
                        AdvanceToId = x.AdvanceToId,
                        Currency = x.Currency
                    },
                    where: x => x.Id == id && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AdvanceTo).Include(x => x.Director)
                );

            return advance;
        }

        public async Task<List<CashAdvanceVM>> GetPersonalAdvances(Guid id)
        {
            var companyAdvances = await _cashAdvanceRepository.GetFilteredList
                (
                    select: x => new CashAdvanceVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        Description = x.Description,
                        RequestedAmount = x.RequestedAmount,
                        Director = x.Director,
                        AdvanceTo = x.AdvanceTo,
                        IsPaymentProcessed = x.IsPaymentProcessed,
                        FinalDateRequest = x.FinalDateRequest,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        DeleteDate = x.DeleteDate,
                        CompanyId = x.CompanyId,
                        InstallmentCount = x.InstallmentCount,
                        AdvanceToId = x.AdvanceToId,
                        DirectorId = x.DirectorId,
                        Currency = x.Currency
                    },
                    where: x => x.AdvanceTo.Id == id && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AdvanceTo).Include(x => x.Director)
                );

            return companyAdvances;
        }

        public async Task<string> GetPersonalName(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var name = $"{user.Name} {user.SecondName} {user.Surname}";

            return name;
        }

        public async Task<CashAdvanceVM> GetVMById(Guid id)
        {
            var advance = await _cashAdvanceRepository.GetFilteredFirstOrDefault
                (
                    select: x => new CashAdvanceVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        Description = x.Description,
                        RequestedAmount = x.RequestedAmount,
                        Director = x.Director,
                        AdvanceTo = x.AdvanceTo,
                        IsPaymentProcessed = x.IsPaymentProcessed,
                        FinalDateRequest = x.FinalDateRequest,
                        CreateDate = x.CreateDate,
                        UpdateDate = x.UpdateDate,
                        DeleteDate = x.DeleteDate,
                        CompanyId = x.CompanyId,
                        AdvanceToId = x.AdvanceToId,
                        InstallmentCount = x.InstallmentCount,
                        DirectorId = x.DirectorId,
                        Currency = x.Currency
                    },
                    where: x => x.Id == id && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.CreateDate),
                    include: x => x.Include(x => x.AdvanceTo).Include(x => x.Director).Include(x => x.Director.Company).Include(x => x.AdvanceTo.Patron)
                );

            return advance;
        }

        public async Task AcceptAdvance(CashAdvanceVM model)
        {
            var map = _mapper.Map<CashAdvance>(model);
            map.Status = Status.Active;
            map.Director = null;
            map.AdvanceTo = null;
            await _cashAdvanceRepository.Update(map);
        }
    }
}
