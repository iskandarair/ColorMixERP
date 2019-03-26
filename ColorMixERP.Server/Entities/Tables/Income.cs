using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Income")]
    public class Income
    {
        public Income()
        {

        }
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }


        [Column(Name = "UserId")]
        public int UserId { get; set; }

        private EntityRef<AccountUser> _user;
        [Association(Storage = "_user", ThisKey = "UserId")]
        public AccountUser User
        {
            get { return _user.Entity; }
            set { _user.Entity = value; }
        }
        //---------------------------------- //
        [Column(Name = "FromWorkPlace")]
        public int FromWorkPlaceId { get; set; }

        private EntityRef<WorkPlace> _fromWorkPlace;
        [Association(Storage = "_fromWorkPlace", ThisKey = "FromWorkPlaceId")]
        public WorkPlace FromWorkPlace
        {
            get { return _fromWorkPlace.Entity; }
            set { _fromWorkPlace.Entity = value; }
        }

        //---------------------------------- //
        [Column(Name = "ToWorkPlace")]
        public int ToWorkplaceId { get; set; }

        private EntityRef<WorkPlace> _toWorkPlace;

        [Association(Storage = "_toWorkPlace", ThisKey = "ToWorkplaceId")]
        public WorkPlace ToWorkPlace
        {
            get { return _toWorkPlace.Entity; }
            set { _toWorkPlace.Entity = value; }
        }
        //---------------------------------- // 

        private EntitySet<IncomeProduct> _incomeProduct { get; set; }

        [Association(Storage = "_incomeProduct", OtherKey = "IncomeId")]
        public EntitySet<IncomeProduct> IncomeProducts
        {
            get { return _incomeProduct; }
            set { _incomeProduct = value; }
        }
        //---------------------------------- // 
        [Column(Name = "CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }

        [Column(Name = "IsProductStock")]
        public bool IsProductStock { get; set; }
    }
}
