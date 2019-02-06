using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "ProductStock")]
    public class ProductStock
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }

        private EntityRef<Product> _Product;
        [Association(Storage = "_Product", ThisKey = "Id")]
        public Product Product
        {
            get { return _Product.Entity; }
            set { _Product.Entity = value; }
        }

        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

    }
}
