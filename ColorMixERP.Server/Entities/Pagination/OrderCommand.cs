using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class OrderCommand : PaginationDTO
    {
        public int SalerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int ClientId { get; set; }
        public int FilterWorkPlaceId { get; set; }

        public bool? SortByOrderDate { get; set; }
        public bool? SortBySalerId { get; set; }
        public bool? SortByClientName { get; set; }
        public bool? SortByPrice { get; set; }
    }
}
