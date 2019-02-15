using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name= "ClientOrder")]
    public class ClientOrder
    {
        public ClientOrder() { }

        public ClientOrder(int salerId, int clientId)
        {
            SalerId = salerId;
            ClientId = clientId;
        }
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        private EntityRef<AccountUser> _Saler;
        [Association(Storage = "_Saler", ThisKey = "SalerId")]
        public AccountUser Saler
        {
            get { return _Saler.Entity; }
            set { _Saler.Entity = value; }
        }

        [Column(Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [Column(Name = "TransactinoId")]
        public string TransactinoId { get; set; }

        [Column(Name = "Saler")]
        private int SalerId { get; set; }
        private EntitySet<Sale> _Sales;
        [Association(Storage = "_Sales", OtherKey = "OrderId")]
        public EntitySet<Sale> Sales
        {
            get { return _Sales;}
            set { _Sales = value; }
        }

        [Column(Name = "PaymentByCash")]
        public decimal PaymentByCash { get; set; }
        [Column(Name = "PaymentByCard")]
        public decimal PaymentByCard { get; set; }
        [Column(Name = "PaymentByTransfer")]
        public decimal PaymentByTransfer { get; set; }

        [Column(Name = "IsDebt")]
        public bool IsDebt { get; set; }

        [Column(Name="ClientId")]
        private int ClientId { get; set; }
        private EntityRef<Client> _Client;

        [Association(Storage = "_Client", ThisKey = "ClientId")]
        public Client Client
        {
            get { return _Client.Entity;}
            set { _Client.Entity = value; }
        }
        public string ClientRepresentitive { get; set; }

        [Column(Name = "OverallPrice")]
        public decimal OverallPrice { get; set; }

        private EntitySet<DebtCover> _DebtCovers;
        [Association(Storage = "_DebtCovers", OtherKey = "Id")]
        public EntitySet<DebtCover> DebtCovers
        {
            get { return _DebtCovers; }
            set { _DebtCovers = value; }
        }


    }
}
