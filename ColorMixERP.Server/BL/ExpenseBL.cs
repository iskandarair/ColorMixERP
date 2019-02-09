using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.BL
{
    public class ExpenseBL
    {
        public List<Expense> GetExpenses()
        {
            return new ExpenseDalFacade().GetExpenses();
        }

        public List<Expense> GetUserExpenses(int userId)
        {
            return new ExpenseDalFacade().GetUserExpenses(userId);
        }
        public Expense GetExpense(int id)
        {
            return new ExpenseDalFacade().GetExpense(id);
        }

        public void Add(int userId, Expense expense)
        {
            new ExpenseDalFacade().Add(userId,expense);
        }

        public void Update(Expense expense)
        {
            new ExpenseDalFacade().Update(expense);
        }

        public void Delete(int expenseId)
        {
            new ExpenseDalFacade().Delete(expenseId);
        }
    }
}
