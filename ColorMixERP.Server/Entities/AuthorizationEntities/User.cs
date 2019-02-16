using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.AuthorizationEntities
{
    public class User
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkplaceId { get; set; }
        public string PosotionRoleId { get; set; }
        public string isSunnat { get; set; }

        public User(int? id,string name)
        {
            Id = id;
            Name = name;
        }
    }
}
