using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class ClientBL
    {
        public List<ClientDTO> GetClients(ClientCommand cmd, int userId, ref int pagesCount)
        {
            if (string.IsNullOrEmpty(cmd.Name))
            {
                cmd.Name = string.Empty;
            }
            if (string.IsNullOrEmpty(cmd.NickName))
            {
                cmd.NickName = string.Empty;
            }
            if (string.IsNullOrEmpty(cmd.INN))
            {
                cmd.INN = string.Empty;
            }
            if (string.IsNullOrEmpty(cmd.City))
            {
                cmd.City = string.Empty;
            }

            var userData = new UserBL().GetAccountUser(userId);
            var workPlace = userData.WorkPlace.Id.Value;
            return new ClientDalFacade().GetClients(cmd, workPlace, ref pagesCount);
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
