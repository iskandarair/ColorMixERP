using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using System.Data.Linq;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.AuthorizationEntities;

namespace ColorMixERP.Server.DAL
{
    public class UserDalFacade
    {
        private LinqContext db = null;
        public UserDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<AccountUserDTO> GetAccountUsers()
        {
            var query =  from c in db.AccountUsers  select new AccountUserDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                PositionRole = c.PositionRole,
                PhoneNumber = c.PhoneNumber,
                WorkPlaceId = c.WorkPlace.Id ?? 0,
                WorkPlaceName = c.WorkPlace.Name,
                isSunnat = c.isSunnat
                //Password = c.Password
            };
            var result = query.ToList();

            return result;
        }
        public AccountUserDTO GetAccountUser(int? id)
        {
            var query = from c in db.AccountUsers where c.Id == id select new AccountUserDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                PositionRole =  c.PositionRole,
                PhoneNumber =  c.PhoneNumber,
                WorkPlaceId =  c.WorkPlace.Id ?? 0,
                WorkPlaceName = c.WorkPlace.Name,
                isSunnat = c.isSunnat
                //Password =  c.Password
            };
            var result = query.FirstOrDefault();

            return result;
        }
        public User GetAccountByName(string name, string password)
        {
            var query = from c in db.AccountUsers where c.Name == name && c.Password ==password  select new User(c.Id, c.Name)
            {
                Name = c.Name,
                SurName = c.Surname,
                PhoneNumber = c.PhoneNumber,
                WorkplaceId = c.WorkPlaceId.ToString(),
                PosotionRoleId = c.PositionRole.ToString(),
                isSunnat =  c.isSunnat.ToString()
            };
            var result = query.FirstOrDefault();

            return result;
        }

        public void Add(AccountUserDTO accountUser)
        {
            var element = new AccountUser(accountUser.Name, accountUser.Surname,accountUser.PositionRole,accountUser.PhoneNumber,accountUser.WorkPlaceId,accountUser.Password);
            element.Expenses = new EntitySet<Expense>();
            db.AccountUsers.InsertOnSubmit(element);
            db.SubmitChanges();
        }
        public void Update(AccountUserDTO user)
        {
            var accountUserToUpdate = (from c in db.AccountUsers where c.Id == user.Id select c).FirstOrDefault();
            accountUserToUpdate.Name = user.Name;
            accountUserToUpdate.Surname = user.Surname;
            accountUserToUpdate.PhoneNumber = user.PhoneNumber;
            accountUserToUpdate.PositionRole = user.PositionRole;
            accountUserToUpdate.WorkPlaceId = user.WorkPlaceId;
            //accountUserToUpdate.Password = user.Password;
            db.SubmitChanges();
        }
        public void Delete(int id)
        {
            var element = (from c in db.AccountUsers where c.Id == id select c).FirstOrDefault();
            db.AccountUsers.DeleteOnSubmit(element);
            db.SubmitChanges();
        }
    }
}
