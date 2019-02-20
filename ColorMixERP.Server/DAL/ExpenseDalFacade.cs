using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.DAL
{
    public class ExpenseDalFacade
    {
        private LinqContext db = null;
        public ExpenseDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<Expense> GetExpenses()
        {
            var query = from c in db.Expenses where c.IsDeleted == false select c;
            return query.ToList();
        }

        public List<Expense> GetUserExpenses(int userId)
        {
            var query = from c in db.AccountUsers where c.Id == userId select c.Expenses;
            return query.FirstOrDefault().ToList();
        }

        public Expense GetExpense(int id)
        {
            var query = from c in db.Expenses where c.Id == id select c;
            return query.FirstOrDefault();
        }

        public void Add(int userId, Expense expense)
        {
            var elementToAdd = new Expense(expense.ExpenseDate,expense.Cost,expense.ExpenseCause,userId);
            db.Expenses.InsertOnSubmit(elementToAdd);
            db.SubmitChanges();
        }

        public void Update(Expense expense)
        {
            var expenseToUpdate = GetExpense(expense.Id);
            expenseToUpdate.Cost = expense.Cost;
            expenseToUpdate.ExpenseCause = expense.ExpenseCause;
            expenseToUpdate.ExpenseDate = expense.ExpenseDate;

            db.SubmitChanges();
        }

        public void Delete(int expenseId)
        {
            var element = (from c in db.Expenses where c.Id == expenseId select c).FirstOrDefault();
            element.IsDeleted = true;
            db.SubmitChanges();
        }
    }
}
