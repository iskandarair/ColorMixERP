using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Utils
{
    public static class DateHelper
    {
        public static DateTime ToSqlDate(this DateTime elemDateTime)
        {
            return  DateTime.Parse(elemDateTime.ToString("d"), new CultureInfo("ru"));
        }
    }
}
