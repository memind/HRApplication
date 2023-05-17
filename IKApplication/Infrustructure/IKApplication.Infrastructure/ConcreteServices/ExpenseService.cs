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


        // company 5e6c2342-a590-4013-a6bb-136808e2c4c9
        // approve e4a69843-c78b-4a13-a103-1ec47b9e1122    mehmet.aydin@google.com
        // expense ea3f5836-171c-4f0b-b26b-43f0597979f3

        public async Task<List<ExpenseVM>> GetExpenseRequests(Guid companyId)
        {
            var expenses = await _expenseRepository.GetDefaults(x => x.Status == Status.Passive);
            List<ExpenseVM> companyExpenses = new List<ExpenseVM>();

            foreach (var expense in expenses)
            {
                if (expense.CompanyId == companyId)
                {
                    var expenseMap = _mapper.Map<ExpenseVM>(expense);
                    companyExpenses.Add(expenseMap);
                }
            }
            return (companyExpenses);
        }

        public async Task<List<ExpenseVM>> GetAllExpenses(Guid companyId)
        {
            var expenses = await _expenseRepository.GetDefaults(x => x.Status == Status.Active || x.Status == Status.Modified);
            List<ExpenseVM> companyExpenses = new List<ExpenseVM>();

            foreach (var expense in expenses)
            {
                if (expense.CompanyId == companyId)
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
                expense.UpdateDate = expenseUpdateDTO.UpdateDate;
                expense.DeleteDate = expenseUpdateDTO.DeleteDate;
                expense.Status = expenseUpdateDTO.Status;
                expense.ShortDescription = expenseUpdateDTO.ShortDescription;
                expense.LongDescription = expenseUpdateDTO.LongDescription;
                expense.Amount = expenseUpdateDTO.Amount;
                expense.ExpenseDate = expenseUpdateDTO.ExpenseDate;
                expense.Type = expenseUpdateDTO.Type;
                await _expenseRepository.Update(expense);
            }
        }

        public async Task<ExpenseVM> GetVMById(Guid id)
        {
            var expense = await _expenseRepository.GetDefault(x => x.Id == id);
            if (expense != null)
            {
                var map = _mapper.Map<ExpenseVM>(expense);
                return map;
            }
            return null;
        }
    }
}