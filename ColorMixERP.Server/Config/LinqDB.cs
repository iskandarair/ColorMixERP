using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Config
{
    public class LinqDB
    {
        private static LinqContext instance = null;
        private static readonly object padlock = new object();

        private LinqDB()
        {
        }

        public static LinqContext Instance
        {
            get
            {
                lock (padlock)
                {

                    if (instance == null)
                    {
                        instance = new LinqContext(LinqContext.DB_CONNECTION);
                    }

                    return instance;
                }
            }
        }
    }
}
