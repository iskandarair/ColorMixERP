using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class ExpenseDalFacade
    {
        private LinqContext db = null;
        public ExpenseDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<Expense> GetExpenses(PaginationDTO cmd, ref int pagesCount)
        {
            var query = from c in db.Expenses where c.IsDeleted == false select c;

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
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
            expenseToUpdate.UpdatedDate = DateTime.Now;

            db.SubmitChanges();
        }

        public void Delete(int expenseId)
        {
            var element = (from c in db.Expenses where c.Id == expenseId select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
