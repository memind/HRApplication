using IKApplication.Application.AbstractRepositories;
using IKApplication.Domain.Entites;
namespace IKApplication.Persistance.ConcreteRepositories
{
    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository(IKAppDbContext iKAppDbContext) : base(iKAppDbContext)
        {
        }
    }
}