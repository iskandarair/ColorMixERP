using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.AuthorizationEntities;

namespace ColorMixERP.Server.BL
{
    public class UserBL
    {
        public List<AccountUserDTO> GetAccountUsers()
        {
            return new UserDalFacade().GetAccountUsers();
        }

        public AccountUserDTO GetAccountUser(int? id)
        {
            return new UserDalFacade().GetAccountUser(id);
        }
    
        public User GetUserByCredentials(string name, string password)
        {
            var user = new UserDalFacade().GetAccountByName(name, password);
            return user;
        }

        public void Add(AccountUserDTO accountUser)
        {
            new UserDalFacade().Add(accountUser);
        }

        public void Update(AccountUserDTO accountUser)
        {
            new UserDalFacade().Update(accountUser);
        }

        public void Delete(int id)
        {
            new UserDalFacade().Delete(id);
        }
    }
}
