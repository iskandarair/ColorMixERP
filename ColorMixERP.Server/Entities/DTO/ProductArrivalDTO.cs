using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ProductArrivalDTO
    {
        public string ProductCode { get; set; }
        public DateTime Date { get; set; }
        public string ProductName { get; set; }
        public string SupplierName { get; set; }
        public decimal Quantity { get; set; }
    }
}
