using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class CompanyInfoDTO
    {
        public int? Id { get; set; }
        public string LegalName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PaymentAccount { get; set; }
        public string BankDetails { get; set; }
        public string City { get; set; }
        public string MFO { get; set; }
        public string INN { get; set; }
        public string OKONX { get; set; }
        public int WorkPlaceId { get; set; }
        
        public string Director { get; set; }
        public string Accountant { get; set; }
    }
}
