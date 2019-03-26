using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class InnerMovementDTO
    {
        public int? Id { get; set; }
        public  DateTime MoveDate { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int FromWorkPlaceId { get; set; }
        public string FromWorkPlaceName { get; set; }
        public int ToWorkPlaceId { get; set; }
        public string ToWorkPlaceName { get; set; }
        // ADDED AFTER Shams gave Stats
        public decimal ProductPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
