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
        public ProductStock() {  }

        public ProductStock(int productId, int workPlaceId)
        {
            this.ProductId = productId;
            this.WorkPlaceId = workPlaceId;
        }
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated =  true)]
        public int Id { get; set; }

        [Column(Name = "ProductId")]
        public int ProductId { get; set; }
        private EntityRef<Product> _Product;
        [Association(Storage = "_Product", ThisKey = "ProductId")]
        public Product Product
        {
            get { return _Product.Entity; }
            set { _Product.Entity = value; }
        }

        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        // HIDDEN COLUMNS 
        [Column(Name = "WorkPlaceId")]
        public int WorkPlaceId { get; set; }

        // =================================================

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        public DateTime? DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
