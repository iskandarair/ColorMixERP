using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.AuthorizationEntities;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class UserBL
    {
        public List<AccountUserDTO> GetAccountUsers(UserCommand cmd, ref int pagesCount)
        {
            if (cmd.Name == null)
            {
                cmd.Name = string.Empty;
            }

            if (cmd.SurName == null)
            {
                cmd.SurName = string.Empty;
            }
            return new UserDalFacade().GetAccountUsers(cmd, ref pagesCount);
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

        public void UpdatePassword(AccountUserDTO user)
        {
            new UserDalFacade().UpdatePassword(user);
        }

        public void Delete(int id)
        {
            new UserDalFacade().Delete(id);
        }
    }
}
