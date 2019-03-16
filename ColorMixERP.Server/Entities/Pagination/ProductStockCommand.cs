using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class ProductStockCommand : PaginationDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int WorkPlaceId { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }

        
        public bool? SortByWorkPlaceId { get; set; }
        public bool? SortByQuantity { get; set; }
    }
}
