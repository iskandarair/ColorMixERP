using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Product")]
    public class Product
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column(Name = "Code")]
        public string Code { get; set; }

        [Column(Name = "Name")]
        public string Name { get; set; }
        
        private EntityRef<Category> _Category;
        [Association(Storage = "_Category", OtherKey = "Id")]
        public Category Category
        {
            get { return _Category.Entity; }
            set { _Category.Entity = value; }
        }

        [Column(Name = "Price")]
        public decimal Price { get; set; }

        [Column(Name = "Currency")]
        public int Currency { get; set; }
        //public Currency Currency { get; set; }

        [Column(Name = "MeasurementUnit")]
        public string MeasurementUnit { get; set; }

        [Column(Name = "BoxedNumber")]
        public decimal BoxedNumber { get; set; }
        [Column(Name="Supplier")]
        private int SupplierId { get; set; }
        private EntityRef<Supplier> _Supplier;
        [Association(Storage = "_Supplier", ThisKey = "SupplierId")]
        public Supplier Supplier
        {
            get { return _Supplier.Entity; }
            set { _Supplier.Entity = value; }
        }

    }
}
