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
        public List<ExpenseDTO> GetExpenses(PaginationDTO cmd, int userId,  ref int pagesCount)
        {
            var userData = new UserBL().GetAccountUser(userId);
            var workplaceId = userData.WorkPlace.Id;
            var isSuperUser = userData.PositionRole == 1 || userData.isSunnat;
            return new ExpenseDalFacade().GetExpenses(cmd, workplaceId.Value, isSuperUser, ref pagesCount);
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
