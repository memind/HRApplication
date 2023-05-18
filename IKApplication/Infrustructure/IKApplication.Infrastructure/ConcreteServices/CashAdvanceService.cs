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

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CashAdvanceService : ICashAdvanceServices
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
            var cashAdvance = _mapper.Map<CashAdvance>(createCashAdvanceDTO);
            cashAdvance.CreateDate = DateTime.Now;
            cashAdvance.Status = Status.Passive;

            await _cashAdvanceRepository.Create(cashAdvance);
        }

        public async Task Update(CashAdvanceUpdateDTO updateCashAdvanceDTO)
        {
            var cashAdvance = await _cashAdvanceRepository.GetDefault(x => x.Id == updateCashAdvanceDTO.Id);

            if (cashAdvance != null)
            {
                cashAdvance.Description = updateCashAdvanceDTO.Description;
                cashAdvance.RequestedAmount = updateCashAdvanceDTO.RequestedAmount;
                //cashAdvance.Director = updateCashAdvanceDTO.Director;
                cashAdvance.UpdateDate = DateTime.Now;
                cashAdvance.Status = updateCashAdvanceDTO.Status;

                await _cashAdvanceRepository.Update(cashAdvance);
            }
        }

        public async Task Delete(Guid id)
        {
            var cashAdvance = await _cashAdvanceRepository.GetDefault(x => x.Id == id);

            if (cashAdvance != null)
            {
                cashAdvance.DeleteDate = DateTime.Now;
                cashAdvance.Status = Status.Deleted;

                await _cashAdvanceRepository.Delete(cashAdvance);
            }
        }

        public async Task<List<CashAdvanceVM>> GetAllAdvances(Guid companyId)
        {
            var advances = await _cashAdvanceRepository.GetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified);
            List<CashAdvanceVM> companyAdvances = new List<CashAdvanceVM>();

            foreach (var advance in advances)
            {
                if (advance.CompanyId == companyId)
                {
                    var advanceMap = _mapper.Map<CashAdvanceVM>(advance);
                    companyAdvances.Add(advanceMap);
                }
            }
            return (companyAdvances);
        }

        public async Task<CashAdvanceUpdateDTO> GetById(Guid id)
        {
            var cashAdvance = await _cashAdvanceRepository.GetDefault(x => x.Id == id);

            if (cashAdvance != null)
            {
                var cashAdvanceDTO = _mapper.Map<CashAdvanceUpdateDTO>(cashAdvance);
                return cashAdvanceDTO;
            }

            return null;
        }

        public async Task<List<CashAdvanceVM>> GetPersonalAdvances(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var advances = await _cashAdvanceRepository.GetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified);
            var userAdvances = advances.Where(x => x.AdvanceToId == user.Id);

            List<CashAdvanceVM> companyAdvances = new List<CashAdvanceVM>();

            foreach (var advance in userAdvances)
            {
                if (advance.CompanyId == user.CompanyId)
                {
                    var advanceMap = _mapper.Map<CashAdvanceVM>(advance);
                    companyAdvances.Add(advanceMap);
                }
            }
            return (companyAdvances);
        }

        public async Task<List<CashAdvanceVM>> GetAdvanceRequests(Guid companyId)
        {
            var advances = await _cashAdvanceRepository.GetDefaults(x => x.Status == Status.Passive);
            List<CashAdvanceVM> companyAdvances = new List<CashAdvanceVM>();

            foreach (var advance in advances)
            {
                if (advance.CompanyId == companyId)
                {
                    var advanceMap = _mapper.Map<CashAdvanceVM>(advance);
                    companyAdvances.Add(advanceMap);
                }
            }
            return (companyAdvances);
        }

        public async Task<CashAdvanceVM> GetVMById(Guid id)
        {
            var advance = await _cashAdvanceRepository.GetDefault(x => x.Id == id);
            if (advance != null)
            {
                var map = _mapper.Map<CashAdvanceVM>(advance);
                return map;
            }
            return null;
        }

        public async Task<string> GetPersonalName(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var name = $"{user.Name} {user.SecondName} {user.Surname}";

            return name;
        }
    }
}
