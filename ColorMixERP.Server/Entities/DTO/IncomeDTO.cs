using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class IncomeDTO
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int FromWorkplaceId { get; set; }
        public string FromWorkplaceName { get; set; }
        public int ToWorkplaceId { get; set; }
        public string ToWorkplaceName { get; set; }
        public List<IncomeProductDTO> IncomeProducts { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

    }
}
