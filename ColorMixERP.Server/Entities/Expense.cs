using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Expense")]
    public class Expense
    {
        public Expense()
        {
        }

        public Expense(DateTime expenseDate, decimal cost, string expenseCause, int userId)
        {
            ExpenseDate = expenseDate;
            Cost = cost;
            ExpenseCause = expenseCause;
            UserId = userId;
        }

        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "ExpenseDate")]
        public DateTime ExpenseDate { get; set; }
        [Column(Name = "Cost")]
        public decimal Cost { get; set; }
        [Column(Name = "ExpenseCause")]
        public string ExpenseCause { get; set; }

        [Column(Name = "UserId")]
        private int UserId { get; set; }



        // =================================================

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        public DateTime? DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
