using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ExpenseDTO
    {
        public  int Id { get; set; }
        public DateTime ExpenseDate { get; set; }
        public string ExpenseCause { get; set; }
        public decimal Cost { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
