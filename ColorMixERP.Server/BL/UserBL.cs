using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.AuthorizationEntities;

namespace ColorMixERP.Server.BL
{
    public class UserBL
    {
        public User GetUserByCredentials(string name, string password)
        {
            var user = new UserDalFacade().GetAccountByName(name, password);
            //AccountUser user = new AccountUser() { Id = 1, Name = "email@domain.com", Password = "password" };
            return user;
        }
        /*
            System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.ASCII.GetBytes("password");
            data = x.ComputeHash(data);
            var passw = System.Text.Encoding.ASCII.GetString(data);
            var isBool = passw == "??.R??`???J$T??";
         */
    }
}
