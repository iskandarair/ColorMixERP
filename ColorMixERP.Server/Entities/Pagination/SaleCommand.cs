using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class SaleCommand : PaginationDTO
    {
        public int ProductId { get; set; }
        public int Currency { get; set; }

        public bool? SortByProduct { get; set; }
        public bool? SortBySalesPrice { get; set; }
        public bool? SortByQuantity { get; set; }
    }
}
