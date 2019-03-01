using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class UserCommand : PaginationDTO
    {
        public int RoleId { get; set; }
        public int WorkPlaceId { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        
        public bool? SortByRoleId { get; set; }
        public bool? SortByWorkPlaceId { get; set; }
        public bool? SortByName { get; set; }
        public bool? SortBySurName { get; set; }

    }
}
