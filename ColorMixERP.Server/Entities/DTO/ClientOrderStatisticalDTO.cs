using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ClientOrderStatisticalDTO
    {
        public List<OrderDTO> ClientOrders { get; set; }
        // A G G R E G A T I O N
        public decimal TotalOverallPrice { get; set; }
        public decimal TotalPaymentCash { get; set; }
        public decimal TotalPaymentCard { get; set; }
        public decimal TotalPaymentTransfer { get; set; }
    }
}
