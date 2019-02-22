using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class ClientBL
    {
        public List<ClientDTO> GetClients()
        {
            return new ClientDalFacade().GetClients();
        }

        public ClientDTO GetClient(int? id)
        {
            return new ClientDalFacade().GetClient(id);
        }

        public void Add(ClientDTO client)
        {
            new ClientDalFacade().Add(client);
        }

        public void Update(ClientDTO client)
        {
            new ClientDalFacade().Update(client);
        }

        public void Delete(int id)
        {
            new ClientDalFacade().Delete(id);
        }
    }
}
