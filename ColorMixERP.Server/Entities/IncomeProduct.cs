using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities
{
    [Table(Name= "IncomeProduct")]
   public class IncomeProduct
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int? Id { get; set; }
        [Column(Name ="IncomeId")]
        public int IncomeId { get; set; }
        [Column(Name = "ProductId")]
        public int ProductId { get; set; }

        private EntityRef<Product> _productId;
        [Association(Storage = "_productId", ThisKey = "ProductId")]
        public Product Product
        {
            get { return _productId.Entity; }
            set { _productId.Entity = value; }
        }

        //---------------------------------- // 
        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        //---------------------------------- // 
        [Column(Name = "CreateDate")]
        public DateTime? CreateDate { get; set; }
        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
