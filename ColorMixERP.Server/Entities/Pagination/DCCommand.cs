using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class DCCommand : PaginationDTO
    {
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public bool? IsDebtor { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? FromUpdatedDate { get; set; }
        public DateTime? ToUpdatedDate { get; set; }

    }
}
