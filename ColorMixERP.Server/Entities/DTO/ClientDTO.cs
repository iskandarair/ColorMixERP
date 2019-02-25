using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ClientDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PaymentAccount { get; set; }
        public string BankDetails { get; set; }
        public string City { get; set; }
        public string MFO { get; set; }
        public string INN { get; set; }
        public string OKONX { get; set; }
    }
}
