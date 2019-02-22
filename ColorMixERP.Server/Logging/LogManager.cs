using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Logging
{
    public class LogManager
    {
        private static ILogger _instance;

        public static ILogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NLogger();
                }

                return _instance;
            }
        }
    }
}
