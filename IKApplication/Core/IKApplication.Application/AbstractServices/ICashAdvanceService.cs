using IKApplication.Application.dtos.CashAdvanceDTOs;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CashAdvanceVMs;
using IKApplication.Application.VMs.ExpenseVMs;

namespace IKApplication.Application.AbstractServices
{
    public interface ICashAdvanceService
    {
        Task Create(CashAdvanceCreateDTO createCashAdvanceDTO);
        Task Update(CashAdvanceUpdateDTO updateCashAdvanceDTO);
        Task Delete(Guid id);
        Task<List<CashAdvanceVM>> GetAllAdvances(Guid companyId);
        Task<List<CashAdvanceVM>> GetPersonalAdvances(Guid id);
        Task<List<CashAdvanceVM>> GetAdvanceRequests(Guid companyId);
        Task<CashAdvanceVM> GetVMById(Guid id);
        Task AcceptAdvance(CashAdvanceVM model);
        Task<CashAdvanceUpdateDTO> GetById(Guid id);
        Task<string> GetPersonalName(Guid id);
    }
}
