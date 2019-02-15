using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "AccountUser")]
    public class AccountUser
    {
        public AccountUser()
        {
        }

        public AccountUser(string name, string surname, int positionRole, string phoneNumber, int workPlaceId, string password)
        {
            Name = name;
            Surname = surname;
            PositionRole = positionRole;
            PhoneNumber = phoneNumber;
            WorkPlaceId = workPlaceId;
            Password = password;
        }
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int? Id { get; set; }

        [Column(Name = "Name")] public string Name { get; set; }
        [Column(Name = "Surname")] public string Surname { get; set; }
        [Column(Name = "PositionRole")] public int PositionRole { get; set; }
        public Role Role { get; set; }

        [Column(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }
        [Column(Name = "WorkPlace")]
        public int WorkPlaceId { get; set; }
        private EntityRef<WorkPlace> _WorkPlace;

        [Association(Storage = "_WorkPlace", ThisKey = "WorkPlaceId")]
        public WorkPlace WorkPlace
        {
            get { return _WorkPlace.Entity; }
            set { _WorkPlace.Entity = value; }
        }
        
        private EntitySet<Expense> _Expenses;

        ///
        [Association(Storage = "_Expenses", OtherKey = "UserId")]
        public EntitySet<Expense> Expenses
        {
            get { return _Expenses; }
            set { _Expenses = value; }
        }

        [Column(Name = "isSunnat")]
        public bool isSunnat { get; set; }

        //For Auth only
        [Column(Name = "Password")]
        public string Password { get; set; }
    }
}
