using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "DailyBalance")]
    public class DailyBalance
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "ProductId")]
        public int ProductId { get; set; }
        [Column(Name = "BalanceDate")]
        public DateTime BalanceDate { get; set; }
        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        private EntityRef<Product> _Products;
        [Association(Storage = "_Products", ThisKey = "ProductId")]
        public Product Product
        {
            get { return _Products.Entity;}
            set { _Products.Entity = Product; }
        }

        [Column(Name = "WorkPlaceId")]
        public int WorkPlaceId { get; set; }
    }
}
