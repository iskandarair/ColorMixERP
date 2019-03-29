using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class InnerMovementGroupDTO
    {
        public DateTime MoveDate { get; set; }
        public int FromWorkPlaceId { get; set; }
        public string FromWorkPlaceName { get; set; }
        public int ToWorkPlaceId { get; set; }
        public string ToWorkPlaceName { get; set; }
        public decimal ProductCount { get; set; }
        public decimal TotalPrice { get; set; }
        public int? GroupId { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
