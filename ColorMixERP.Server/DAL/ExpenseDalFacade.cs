using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    public class ExpenseDalFacade
    {
        private LinqContext db = null;
        public ExpenseDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ExpenseDTO> GetExpenses(PaginationDTO cmd,int workplaceId, bool isSuperUser, ref int pagesCount)
        {
            IQueryable<ExpenseDTO> query;
            if (isSuperUser)
            {
                 query = from c in db.Expenses
                    join user in db.AccountUsers on c.UserId equals user.Id
                    select new ExpenseDTO()
                    {
                        Id = c.Id,
                        Cost = c.Cost,
                        ExpenseCause = c.ExpenseCause,
                        ExpenseDate = c.ExpenseDate,
                        UserId = c.UserId,
                        UserName = user.Name + " " + user.Surname
                    };
            }
            else
            {
                   query = from c in db.Expenses
                    join user in db.AccountUsers on c.UserId equals user.Id
                    where c.IsDeleted == false && user.WorkPlaceId == workplaceId
                    select new ExpenseDTO()
                    {
                        Id = c.Id,
                        Cost = c.Cost,
                        ExpenseCause = c.ExpenseCause,
                        ExpenseDate = c.ExpenseDate,
                        UserId = c.UserId,
                        UserName = user.Name + " " + user.Surname
                    };
            }

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public List<ExpenseDTO> GetUserExpenses(int userId)
        {
            var query = from c in db.Expenses where c.UserId == userId
                join user in db.AccountUsers on c.UserId equals user.Id
                        select new ExpenseDTO()
                {
                    Id = c.Id,
                    Cost = c.Cost,
                    ExpenseCause = c.ExpenseCause,
                    ExpenseDate = c.ExpenseDate,
                    UserId = c.UserId,
                    UserName = user.Name + " " + user.Surname
                };// c.Expenses;
            return query.ToList();
        }

        public ExpenseDTO GetExpense(int id)
        {
            var query = from c in db.Expenses where c.Id == id
                join user in db.AccountUsers on c.UserId equals user.Id
                        select new ExpenseDTO()
                {
                    Id = c.Id,
                    Cost = c.Cost,
                    ExpenseCause = c.ExpenseCause,
                    ExpenseDate = c.ExpenseDate,
                    UserId = c.UserId,
                    UserName = user.Name + " " + user.Surname
                        };
            return query.FirstOrDefault();
        }

        public void Add(int userId, ExpenseDTO expense)
        {
            var elementToAdd = new Expense(expense.ExpenseDate,expense.Cost,expense.ExpenseCause,userId);
            db.Expenses.InsertOnSubmit(elementToAdd);
            db.SubmitChanges();
        }

        public void Update(ExpenseDTO expense)
        {
            var query = from c in db.Expenses where c.Id == expense.Id select c;
            var expenseToUpdate = query.FirstOrDefault();
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
