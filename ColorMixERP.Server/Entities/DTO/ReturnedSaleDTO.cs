using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ReturnedSaleDTO
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Cause { get; set; }
        public decimal DefectedQuantity { get; set; }
        public decimal Quantity { get; set; }
        public decimal ReturnedPrice { get; set; }


    }
}
