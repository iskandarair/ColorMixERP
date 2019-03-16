using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class AccountUserDTO
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionRole { get; set; }
        
        public string PhoneNumber { get; set; }
        public WorkPlaceDTO WorkPlace { get; set; }
        //public int WorkPlaceId { get; set; }
        //public string WorkPlaceName { get; set; }
        public string Password { get; set; }

        public bool isSunnat { get; set; }
    }
}
