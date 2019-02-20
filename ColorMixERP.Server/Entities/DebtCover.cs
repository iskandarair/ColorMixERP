using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "DebtCover")]
    public class DebtCover 
    {
        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "CoverDate")]
        public DateTime CoverDate { get; set; }
        [Column(Name = "PaymentByCash")]
        public decimal PaymentByCash { get; set; }
        [Column(Name = "PaymentByCard")]
        public decimal PaymentByCard { get; set; }
        [Column(Name = "PaymentByTransfer")]
        public decimal PaymentByTransfer { get; set; }
        [Column(Name="OrderId")]
        private int OrderId { get; set; }

        // =================================================

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        public DateTime DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime UpdatedDate { get; set; }
    }
}
