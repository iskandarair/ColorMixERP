using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class DailyBalanceCommand : PaginationDTO
    {
        public DateTime? Date { get; set; }
        /// <summary>
        /// First Date to return. Not in between From\To
        /// </summary>
        public DateTime? FromDate { get; set; }
        /// <summary>
        /// Second Date to return. Not in between From\To
        /// </summary>
        public DateTime? ToDate { get; set; }

        public int? TargetWorkPlace { get; set; }
    }
}
