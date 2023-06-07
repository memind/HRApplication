using AutoMapper;
using IKApplication.Application.AbstractRepositories;
using IKApplication.Application.AbstractServices;
using IKApplication.Application.dtos.ExpenseDTOs;
using IKApplication.Application.VMs.ExpenseVMs;
using IKApplication.Domain.Entites;
using IKApplication.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace IKApplication.Infrastructure.ConcreteServices
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        public ExpenseService(IMapper mapper, IExpenseRepository expenseRepository, IAppUserService appUserService)
        {
            _mapper = mapper;
            _expenseRepository = expenseRepository;
            _appUserService = appUserService;
        }

        public async Task CreateExpense(ExpenseCreateDTO expenseCreateDTO)
        {
            var map = _mapper.Map<Expense>(expenseCreateDTO);
            var expenseBy = await _appUserService.GetCurrentUserInfo(expenseCreateDTO.ExpenseById);

            map.Amount = Convert.ToDecimal(expenseCreateDTO.AmountString);
            map.ApprovedById = (Guid)expenseBy.PatronId;
            map.ExpenseById = expenseBy.Id;
            map.CompanyId = expenseBy.CompanyId;

            await _expenseRepository.Create(map);
        }

        public async Task UpdateExpense(ExpenseUpdateDTO expenseUpdateDTO)
        {
            var expense = await _expenseRepository.GetDefault(x => x.Id == expenseUpdateDTO.Id);
            if (expense != null)
            {
                expense.Status = Status.Passive;
                expense.UpdateDate = expenseUpdateDTO.UpdateDate;
                expense.DeleteDate = expenseUpdateDTO.DeleteDate;
                expense.ShortDescription = expenseUpdateDTO.ShortDescription;
                expense.LongDescription = expenseUpdateDTO.LongDescription;
                expense.Amount = Convert.ToDecimal(expenseUpdateDTO.AmountString);
                expense.ExpenseDate = expenseUpdateDTO.ExpenseDate;
                expense.Type = expenseUpdateDTO.Type;
                expense.Currency = expenseUpdateDTO.Currency;
                await _expenseRepository.Update(expense);
            }
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
            var companyExpenses = await _expenseRepository.GetFilteredList
                (
                    select: x => new ExpenseVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        CreateDate = x.CreateDate,
                        ShortDescription = x.ShortDescription,
                        LongDescription = x.LongDescription,
                        Amount = x.Amount,
                        ExpenseDate = x.ExpenseDate,
                        ApprovedById = x.ApprovedById,
                        ApprovedBy = x.ApprovedBy,
                        CompanyId = x.CompanyId,
                        ExpenseById = x.ExpenseById,
                        ExpenseBy = x.ExpenseBy,
                        Type = x.Type,
                        Currency = x.Currency
                    },
                    where: x => x.CompanyId == companyId && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.ExpenseDate),
                    include: x => x.Include(x => x.ExpenseBy).Include(x => x.ApprovedBy)
                );

            return companyExpenses;
        }

        public async Task<ExpenseUpdateDTO> GetById(Guid id)
        {
            var expense = await _expenseRepository.GetFilteredFirstOrDefault
                (
                    select: x => new ExpenseUpdateDTO()
                    {
                        Id = x.Id,
                        ShortDescription = x.ShortDescription,
                        LongDescription = x.LongDescription,
                        AmountString = x.Amount.ToString(),
                        ExpenseDate = x.ExpenseDate,
                        Type = x.Type,
                        Currency = x.Currency
                    },
                    where: x => x.Id == id && (x.Status != Status.Deleted),
                    orderBy: x => x.OrderBy(x => x.ExpenseDate),
                    include: x => x.Include(x => x.ExpenseBy).Include(x => x.ApprovedBy)
                );

            return expense;
        }

        public async Task<List<ExpenseVM>> GetExpenseRequests(Guid companyId)
        {
            var companyExpenses = await _expenseRepository.GetFilteredList
                (
                    select: x => new ExpenseVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        CreateDate = x.CreateDate,
                        ShortDescription = x.ShortDescription,
                        LongDescription = x.LongDescription,
                        Amount = x.Amount,
                        ExpenseDate = x.ExpenseDate,
                        ApprovedById = x.ApprovedById,
                        ApprovedBy = x.ApprovedBy,
                        CompanyId = x.CompanyId,
                        ExpenseById = x.ExpenseById,
                        ExpenseBy = x.ExpenseBy,
                        Type = x.Type,
                        Currency = x.Currency
                    },
                    where: x => x.Status == Status.Passive && x.ExpenseBy.CompanyId == companyId && x.ApprovedById == x.ExpenseBy.PatronId,
                    orderBy: x => x.OrderBy(x => x.ExpenseDate),
                    include: x => x.Include(x => x.ExpenseBy).Include(x => x.ApprovedBy).Include(x => x.ExpenseBy.Patron)
                );

            return companyExpenses;
        }

        public async Task<List<ExpenseVM>> GetPersonalExpenses(Guid id)
        {
            var companyExpenses = await _expenseRepository.GetFilteredList
                (
                    select: x => new ExpenseVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        CreateDate = x.CreateDate,
                        ShortDescription = x.ShortDescription,
                        LongDescription = x.LongDescription,
                        Amount = x.Amount,
                        ExpenseDate = x.ExpenseDate,
                        ApprovedById = x.ApprovedById,
                        ApprovedBy = x.ApprovedBy,
                        ExpenseById = x.ExpenseById,
                        CompanyId = x.CompanyId,
                        ExpenseBy = x.ExpenseBy,
                        Type = x.Type,
                        Currency = x.Currency
                    },
                    where: x => x.ExpenseBy.Id == id && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.ExpenseDate),
                    include: x => x.Include(x => x.ExpenseBy).Include(x => x.ApprovedBy)
                );

            return companyExpenses;
        }

        public async Task<string> GetPersonalName(Guid id)
        {
            var user = await _appUserService.GetById(id);
            var name = $"{user.Name} {user.SecondName} {user.Surname}";

            return name;
        }

        public async Task<ExpenseVM> GetVMById(Guid id)
        {
            var expense = await _expenseRepository.GetFilteredFirstOrDefault
                (
                    select: x => new ExpenseVM()
                    {
                        Id = x.Id,
                        Status = x.Status,
                        CreateDate = x.CreateDate,
                        ShortDescription = x.ShortDescription,
                        LongDescription = x.LongDescription,
                        Amount = x.Amount,
                        ExpenseDate = x.ExpenseDate,
                        ApprovedById = x.ApprovedById,
                        ApprovedBy = x.ApprovedBy,
                        ExpenseById = x.ExpenseById,
                        CompanyId = x.CompanyId,
                        ExpenseBy = x.ExpenseBy,
                        Type = x.Type,
                        Currency = x.Currency
                    },
                    where: x => x.Id == id && x.Status != Status.Deleted,
                    orderBy: x => x.OrderBy(x => x.ExpenseDate),
                    include: x => x.Include(x => x.ExpenseBy).Include(x => x.ApprovedBy).Include(x => x.ExpenseBy.Company).Include(x => x.ExpenseBy.Patron)
                );

            return expense;
        }

        public async Task AcceptExpense(ExpenseVM model)
        {
            var map = _mapper.Map<Expense>(model);
            map.Status = Status.Active;
            map.ApprovedBy = null;
            map.ExpenseBy = null;
            await _expenseRepository.Update(map);
        }
    }
}
