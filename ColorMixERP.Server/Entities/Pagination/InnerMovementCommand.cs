using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.Pagination
{
    public class InnerMovementCommand : PaginationDTO
    {
        public int ProductId { get; set; }
        public int FilterWorkPlaceId { get; set; }
        public int FromPlaceId { get; set; }
        public int ToPlaceId { get; set; }
        public DateTime? MoveDate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public bool? SortByProduct { get; set; }
        public bool? SortByDate { get; set; }
        public bool? SortByFromPlace { get; set; }
        public bool? SortByToPlace { get; set; }

    }
}
