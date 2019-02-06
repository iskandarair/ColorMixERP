using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Sale")]
    public class Sale
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        private EntityRef<Product> _Product;
        [Association(Storage = "_Product", ThisKey = "Id")]
        public Product Product
        {
            get { return _Product.Entity; }
            set { this._Product.Entity = value; }
        }

        [Column(Name = "ProductName")]
        public string ProductName { get; set; }

        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Column(Name = "ProductPrice")]
        public decimal ProductPrice { get; set; }

        [Column(Name = "SalesPrice")]
        public decimal SalesPrice { get; set; }
    }
}
