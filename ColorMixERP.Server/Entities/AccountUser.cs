﻿using System;
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
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int? Id { get; set; }

        [Column(Name = "Name")] public string Name { get; set; }
        [Column(Name = "Surname")] public string Surname { get; set; }
        [Column(Name = "PositionRole")] public int PositionRole { get; set; }
        public Role Role { get; set; }

        [Column(Name = "PhoneNumber")] public string PhoneNumber { get; set; }
        //

        private EntityRef<WorkPlace> _WorkPlace;

        [Association(Storage = "_WorkPlace", OtherKey = "Id")]
        public WorkPlace WorkPlace
        {
            get { return _WorkPlace.Entity; }
            set { _WorkPlace.Entity = value; }
        }
        
        private EntitySet<Expense> _Expenses;

        ///
        [Association(Storage = "_Expenses", OtherKey = "Id")]
        public EntitySet<Expense> Expenses
        {
            get { return _Expenses; }
            set { _Expenses = value; }
        }


        //For Auth only
        [Column(Name = "Password")]
        public string Password { get; set; }
    }
}
