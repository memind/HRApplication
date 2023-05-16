using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using IKApplication.Persistance.ConcreteRepositories;
using Microsoft.AspNetCore.Identity;
namespace IKApplication.Infrastructure.ConcreteServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        public ExpenseService(IMapper mapper, IExpenseRepository expenseRepository)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
        }
        public async Task CreateExpense(ExpenseCreateDTO expenseCreateDTO)
        {
            var map = _mapper.Map<Expense>(expenseCreateDTO);
            await _expenseRepository.Create(map);
        }
        public async Task DeleteExpense(Guid id)
        {
            var expense = await _expenseRepository.GetDefault(x => x.Id == id);
            expense.DeleteDate = DateTime.Now;
            expense.Status = Status.Deleted;
            await _expenseRepository.Delete(expense);
        }
        public async Task<List<ExpenseVM>> GetAllExpenses(Guid companyId)
        {
            var expenses = await _expenseRepository.GetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified);
            List<ExpenseVM> companyExpenses = new List<ExpenseVM>();

            foreach (var expense in expenses)
            {
                if (expense.ExpenseBy.CompanyId == companyId)
                {
                    var expenseMap = _mapper.Map<ExpenseVM>(expense);
                    companyExpenses.Add(expenseMap);
                }
            }
            return (companyExpenses);
        }
        public async Task<ExpenseUpdateDTO> GetById(Guid id)
        {
            var expense = await _expenseRepository.GetDefault(x => x.Id == id);
            if (expense != null)
            {
                var map = _mapper.Map<ExpenseUpdateDTO>(expense);
                return map;
            }
            return null;
        }
        public async Task UpdateExpense(ExpenseUpdateDTO expenseUpdateDTO)
        {
            var expense = await _expenseRepository.GetDefault(x => x.Id == expenseUpdateDTO.Id);
            if (expense != null)
            {
                expense.CreateDate = expenseUpdateDTO.CreateDate;
                expense.UpdateDate = expenseUpdateDTO.UpdateDate;
                expense.DeleteDate = expenseUpdateDTO.DeleteDate;
                expense.Status = expenseUpdateDTO.Status;
                expense.ShortDescription = expenseUpdateDTO.ShortDescription;
                expense.LongDescription = expenseUpdateDTO.LongDescription;
                expense.Amount = expenseUpdateDTO.Amount;
                expense.ExpenseDate = expenseUpdateDTO.ExpenseDate;
                expense.ApprovedById = expenseUpdateDTO.ApprovedById;
                expense.ExpenseById = expenseUpdateDTO.ExpenseById;
                expense.Type = expenseUpdateDTO.Type;
                await _expenseRepository.Update(expense);
            }
        }
    }
}