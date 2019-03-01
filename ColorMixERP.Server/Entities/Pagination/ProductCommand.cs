using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class ProductCommand : PaginationDTO
    {
        // F I L T E R
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string SupplierName { get; set; }
        // S O R T
        public bool? SortByProductCode { get; set; }
        public bool? SortByproductName { get; set; }
        public bool? SortByCategory { get; set; }
        public bool? SortBySupplier { get; set; }
    }
}
