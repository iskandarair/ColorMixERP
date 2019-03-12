using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class IncomeCommand : PaginationDTO
    {
        public int UserId { get; set; }
        public int FromWorkplace { get; set; }
        public int ToWorkplace { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
