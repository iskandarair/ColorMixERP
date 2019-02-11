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
        public InnerMovement()
        {
        }

        public InnerMovement(int? id, DateTime moveDate, int productId, decimal quantity, int fromWOrkPlaceId,
            int toWorkPlaceId)
        {
            Id = id;
            MoveDate = moveDate;
            ProductId = productId;
            Quantity = quantity;
            FromWOrkPlaceId = fromWOrkPlaceId;
            ToWorkPlaceId = toWorkPlaceId;
        }

        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int? Id { get; set; }
        [Column(Name = "MoveDate")]
        public DateTime MoveDate { get; set; }
        [Column(Name="ProductId")]
        private int ProductId { get; set; }
        private EntityRef<Product> _Product;
        [Association(Storage = "_Product", ThisKey = "ProductId")]
        public Product Product
        {
            get { return _Product.Entity; }
            set { _Product.Entity = value; }
        }
        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Column(Name = "FromWOrkPlaceId")]
        private int FromWOrkPlaceId { get; set; }
        private EntityRef<WorkPlace> _FromWorkPlace;
        [Association(Storage = "_FromWorkPlace", ThisKey = "FromWOrkPlaceId")]
        public WorkPlace FromWorkPlace
        {
            get { return _FromWorkPlace.Entity; }
            set { _FromWorkPlace.Entity = value; }
        }
        [Column(Name="ToWorkPlaceId")]
        private int ToWorkPlaceId { get; set; }
        private EntityRef<WorkPlace> _ToWorkPlace;
        [Association(Storage = "_ToWorkPlace", ThisKey = "ToWorkPlaceId")]
        public WorkPlace ToWorkPlace
        {
            get { return _ToWorkPlace.Entity; }
            set { _ToWorkPlace.Entity = value; }
        }
    }
}
