using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public int SalerId { get; set; }
        public string WorkPlaceName { get; set; }
        public string WorkPlaceLocation { get; set; }
        public string SalerName { get; set; }
        public DateTime OrderDate { get; set; }
        public List<SaleDTO> Sales { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientRepresentitive { get; set; }
        public decimal OverallPrice { get; set; }
        public decimal PaymentByCash { get; set; }
        public decimal PaymentByCard { get; set; }
        public decimal PaymentByTransfer { get; set; }
        public bool IsDebt { get; set; }
    }
}
