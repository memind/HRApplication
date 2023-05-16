using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class CashAdvanceService : ICashAdvanceServices
    {
        private readonly ICashAdvanceRepository _cashAdvanceRepository;
        private readonly IMapper _mapper;

        public CashAdvanceService(ICashAdvanceRepository cashAdvanceRepository, IMapper mapper)
        {
            _cashAdvanceRepository = cashAdvanceRepository;
            _mapper = mapper;
        }

        public async Task Create(CashAdvanceCreateDTO createCashAdvanceDTO)
        {
            var cashAdvance = _mapper.Map<CashAdvance>(createCashAdvanceDTO);
            cashAdvance.Id = Guid.NewGuid();
            cashAdvance.CreateDate = DateTime.Now;
            cashAdvance.Status = Status.Active;

            await _cashAdvanceRepository.Create(cashAdvance);
        }

        public async Task Update(CashAdvanceUpdateDTO updateCashAdvanceDTO)
        {
            var cashAdvance = await _cashAdvanceRepository.GetDefault(x => x.Id == updateCashAdvanceDTO.Id);

            if (cashAdvance != null)
            {
                cashAdvance.Description = updateCashAdvanceDTO.Description;
                cashAdvance.RequestedAmount = updateCashAdvanceDTO.RequestedAmount;
                cashAdvance.Director = updateCashAdvanceDTO.Director;
                cashAdvance.UpdateDate = DateTime.Now;

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

        public async Task<List<CashAdvanceVM>> GetAllAdvances()
        {
            var cashAdvances = await _cashAdvanceRepository.GetFilteredList(
                select: x => new CashAdvanceVM
                {
                    Id = x.Id,
                    Description = x.Description,
                    RequestedAmount = x.RequestedAmount,
                    Director = x.Director,
                    IsPaymentProcessed = x.IsPaymentProcessed,
                    FinalDateRequest = x.FinalDateRequest
                },
                where: x => x.Status == Status.Active);

            return cashAdvances;
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

        public Task Update(CashAdvanceCreateDTO updateCashAdvanceDTO)
        {
            throw new NotImplementedException();
        }
    }
}
