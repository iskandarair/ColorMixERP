using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "InnerMovement")]
    public class InnerMovement
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column(Name = "MoveDate")]
        public DateTime MoveDate { get; set; }

        private EntityRef<Product> _Product;
        [Association(Storage = "_Product", OtherKey = "Id")]
        public Product Product
        {
            get { return _Product.Entity; }
            set { _Product.Entity = value; }
        }
        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        private EntityRef<WorkPlace> _FromWorkPlace;
        [Association(Storage = "_FromWorkPlace", ThisKey = "Id")]
        public WorkPlace FromWorkPlace
        {
            get { return _FromWorkPlace.Entity; }
            set { _FromWorkPlace.Entity = value; }
        }

        private EntityRef<WorkPlace> _ToWorkPlace;
        [Association(Storage = "_ToWorkPlace", ThisKey = "Id")]
        public WorkPlace ToWorkPlace
        {
            get { return _ToWorkPlace.Entity; }
            set { _ToWorkPlace.Entity = value; }
        }
    }
}
