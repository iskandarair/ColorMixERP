using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "WorkPlace")]
    public class WorkPlace
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int? Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Location")]
        public string Location { get; set; }
        ///
        private EntitySet<ProductStock> _ProductStock;

        [Association(Storage = "_ProductStock", OtherKey = "Id")]
        public EntitySet<ProductStock> ProductStock
        {
            get { return _ProductStock; }
            set { _ProductStock = value; }
        }
    }
}
