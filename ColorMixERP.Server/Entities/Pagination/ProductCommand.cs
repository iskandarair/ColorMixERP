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
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        // S O R T
        public bool? SortByProductCode { get; set; }
        public bool? SortByproductName { get; set; }
        public bool? SortByCategory { get; set; }
        public bool? SortBySupplier { get; set; }
    }
}
