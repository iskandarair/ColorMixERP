using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
        public int Currency { get; set; }
        public string MeasurementUnit { get; set; }
        public decimal BoxedNumber { get; set; }
        public Supplier Supplier { get; set; }
    }
}
