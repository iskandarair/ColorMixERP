using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class DebtCoverDTO
    {
        public int Id { get; set; }
        public DateTime CoverDate { get; set; }
        public decimal PaymentByCash { get; set; }
        public decimal PaymentByCard { get; set; }
        public decimal PaymentByTransfer { get; set; }
        public int OrderId { get; set; }
    }
}
