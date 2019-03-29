using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Utils
{
    public static class PagingUtils
    {
        public static IEnumerable<T> Page<T>(this IEnumerable<T> en, double pageSize, int page)
        {
            return en.Skip((page-1) * ((int) pageSize)).Take((int)pageSize);
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> qu, double pageSize, int page)
        {
            return qu.Skip(((int)pageSize) * (page - 1)).Take((int)pageSize);
        }
    }
}
