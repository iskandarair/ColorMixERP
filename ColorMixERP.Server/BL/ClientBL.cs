using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;

namespace ColorMixERP.Server.BL
{
    public class ClientBL
    {
        public List<Client> GetClients()
        {
            return new ClientDalFacade().GetClients();
        }

        public Client GetClient(int? id)
        {
            return new ClientDalFacade().GetClient(id);
        }

        public void Add(Client client)
        {
            new ClientDalFacade().Add(client);
        }

        public void Update(Client client)
        {
            new ClientDalFacade().Update(client);
        }

        public void Delete(int id)
        {
            new ClientDalFacade().Delete(id);
        }
    }
}
