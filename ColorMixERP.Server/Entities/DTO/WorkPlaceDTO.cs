using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class WorkPlaceDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public List<ProductStockDTO> ProductStocks { get; set; }
        public bool IsDeleted { get; set; }
    }
}
