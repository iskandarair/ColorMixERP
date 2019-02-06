using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Category")]
    public class Category
    {
        [Column(Name = "Id", IsPrimaryKey = true)]
        public int? Id { get; set; }

        [Column(Name = "Code")]
        public string Code { get; set; }
        [Column(Name = "Name")]
        public  string Name { get; set; }
    }
}
