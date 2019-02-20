using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "ReturnedSale")]
    public class ReturnedSale
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name="SaleId")]
        private int SaleId { get; set; }
        private EntityRef<Sale> _Sale;
        [Association(Storage = "_Sale", ThisKey = "SaleId")]
        public Sale Sale
        {
            get { return _Sale.Entity; }
            set { _Sale.Entity = value; }
        }

        [Column(Name = "PhoneNumber")]
        public DateTime ReturnDate { get; set; }

        [Column(Name = "Cause")]
        public string Cause { get; set; }

        [Column(Name = "DefectedQuantity")]
        public decimal DefectedQuantity { get; set; }

        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Column(Name = "ReturnedPrice")]
        public decimal ReturnedPrice { get; set; }

        // =================================================

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        public DateTime DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }

    }
}
