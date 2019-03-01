using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class ClientCommand : PaginationDTO
    {
        public string Name { get; set; }
        public string NickName { get; set; }
        public string INN { get; set; }
        public string City { get; set; }

        public bool? SortByName { get; set; }
        public bool? SortByNickName { get; set; }
        public bool? SortByCity { get; set; }

    }
}
