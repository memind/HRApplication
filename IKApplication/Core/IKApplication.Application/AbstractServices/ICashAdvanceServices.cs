using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ICashAdvanceServices
    {
        Task Create(CashAdvanceCreateDTO createCashAdvanceDTO);
        Task Update(CashAdvanceCreateDTO updateCashAdvanceDTO);
        Task Delete(Guid id);
        Task<List<CashAdvanceVM>> GetAllAdvances();
        Task<CashAdvanceUpdateDTO> GetById(Guid id);
    }
}
