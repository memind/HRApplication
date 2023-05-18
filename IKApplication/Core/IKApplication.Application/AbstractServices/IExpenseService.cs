using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.DTOs.CompanyDTOs;
using IKApplication.Application.VMs.CompanyVMs;
using IKApplication.Application.VMs.ExpenseVMs;
namespace IKApplication.Application.AbstractServices
{
    public interface IExpenseService
    {
        Task CreateExpense(ExpenseCreateDTO expenseCreateDTO);
        Task UpdateExpense(ExpenseUpdateDTO expenseUpdateDTO);
        Task DeleteExpense(Guid id);
        Task<List<ExpenseVM>> GetAllExpenses(Guid companyId);
        Task<List<ExpenseVM>> GetPersonalExpenses(Guid id);
        Task<List<ExpenseVM>> GetExpenseRequests(Guid companyId);
        Task<ExpenseVM> GetVMById(Guid id);
        Task<ExpenseUpdateDTO> GetById(Guid id);
        Task<string> GetPersonalName(Guid id);
    }
}