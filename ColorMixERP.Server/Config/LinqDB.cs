using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ColorMixERP.Server.Config
{
    public class LinqDB
    {
        private static string instance = null;
        private static readonly object padlock = new object();

        private LinqDB()
        {
        }

        public static string Instance
        {
            get
            {
                lock (padlock)
                {

                    if (instance == null)
                    {
                        instance = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
                    }

                    return instance;
                }
            }
        }
    }
}
