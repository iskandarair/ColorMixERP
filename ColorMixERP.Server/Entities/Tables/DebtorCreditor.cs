using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities
{
    [Table(Name= "DebtorCreditor")]
    public class DebtorCreditor
    {
        [Column(Name = "Id", IsDbGenerated = true, IsPrimaryKey =  true)]
        public int Id { get; set; }
        [Column(Name="Amount")]
        public decimal Amount { get; set; }
        [Column(Name="IsDebtor")]
        public bool isDebtor { get; set; }
        [Column(Name= "CreatedDate")]
        public DateTime CreatedDate { get; set; }
        [Column(Name = "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
        // C L I E N T   D E T A I L S  
        [Column(Name = "ClientId")]
        public int ClientId { get; set; }
        private EntityRef<Client> _client;
        [Association(Storage = "_client", ThisKey = "ClientId")]
        public Client Client {
            get { return _client.Entity; }
            set { _client.Entity = value; }
        }

    }
}
