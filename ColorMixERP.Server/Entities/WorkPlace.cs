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
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated =  true)]
        public int? Id { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "Location")]
        public string Location { get; set; }
        ///
        private EntitySet<ProductStock> _ProductStock;

        [Association(Storage = "_ProductStock", OtherKey = "WorkPlaceId", IsForeignKey = true)]
        public EntitySet<ProductStock> ProductStock
        {
            get { return _ProductStock; }
            set { _ProductStock = value; }
        }


        // =================================================

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        public DateTime? DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
