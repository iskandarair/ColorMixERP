using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Tables
{
    [Table(Name = "CompanyInfo")]
    public class CompanyInfo
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int? Id { get; set; }
        [Column(Name = "LegalName")]
        public string LegalName { get; set; }
        [Column(Name = "Address")]
        public string Address { get; set; }
        [Column(Name = "Phone")]
        public string Phone { get; set; }
        [Column(Name = "PaymentAccount")]
        public string PaymentAccount { get; set; }
        [Column(Name = "BankDetails")]
        public string BankDetails { get; set; }
        [Column(Name = "City")]
        public string City { get; set; }
        [Column(Name = "MFO")]
        public string MFO { get; set; }
        [Column(Name = "INN")]
        public string INN { get; set; }
        [Column(Name = "OKONX")]
        public string OKONX { get; set; }

        [Column(Name = "Director")]
        public string Director { get; set; }
        [Column(Name = "Accountant")]
        public string Accountant { get; set; }

        // =================================================


        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]

        public DateTime? DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

    }
}
