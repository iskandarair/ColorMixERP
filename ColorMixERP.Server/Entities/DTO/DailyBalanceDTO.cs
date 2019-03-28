using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class DailyBalanceDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime BalanceDate { get; set; }
        public ProductDTO Product { get; set; }
        public decimal Quantity { get; set; }
        public int WorkPlaceId { get; set; }
        public WorkPlaceDTO WorkPlace { get; set; }
        public decimal TotalPrice
        {
            get { return Product.Price * Quantity;}
        }
    }
}
