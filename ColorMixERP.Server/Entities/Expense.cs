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
    }
}
