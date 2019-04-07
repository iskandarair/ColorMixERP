using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Client")]
    public class Client
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int? Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "NickName")]
        public string NickName { get; set; }
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

        // =================================================
        [Column(Name= "DebtorCreditor")]
        public decimal DebtorCreditor { get; set; }

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        
        public DateTime? DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Column(Name = "WorkPlaceId")]
        public int WorkPlaceId { get; set; }

        private EntityRef<WorkPlace> _WorkPlace;
        [Association(Storage = "_WorkPlace", ThisKey = "WorkPlaceId")]
        public WorkPlace WorkPlace
        {
            get { return _WorkPlace.Entity; }
            set { _WorkPlace.Entity = value; }
        }
    }
}
