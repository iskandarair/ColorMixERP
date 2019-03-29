using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class ExpenseBL
    {
        public List<ExpenseDTO> GetExpenses(PaginationDTO cmd, ref int pagesCount)
        {
            return new ExpenseDalFacade().GetExpenses(cmd, ref pagesCount);
        }

        public List<ExpenseDTO> GetUserExpenses(int userId)
        {
            return new ExpenseDalFacade().GetUserExpenses(userId);
        }
        public ExpenseDTO GetExpense(int id)
        {
            return new ExpenseDalFacade().GetExpense(id);
        }

        public void Add(int userId, ExpenseDTO expense)
        {
            new ExpenseDalFacade().Add(userId,expense);
        }

        public void Update(ExpenseDTO expense)
        {
            new ExpenseDalFacade().Update(expense);
        }

        public void Delete(int expenseId)
        {
            new ExpenseDalFacade().Delete(expenseId);
        }
    }
}
