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
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class UserDalFacade
    {
        private LinqContext db = null;
        public UserDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<AccountUserDTO> GetAccountUsers(UserCommand cmd, ref int pagesCount)
        {
            var query =  from c in db.AccountUsers
                where c.IsDeleted == false &&
                      c.Name.Contains(cmd.Name) &&
                      c.Surname.Contains(cmd.SurName)
                         select new AccountUserDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                PositionRole = c.PositionRole,
                PhoneNumber = c.PhoneNumber,
                WorkPlace = new WorkPlaceDTO()
                {
                    Id = c.WorkPlace.Id,
                    Name = c.WorkPlace.Name,
                    Location = c.WorkPlace.Location,
                    IsDeleted = c.IsDeleted,
                },
                isSunnat = c.isSunnat
                //Password = c.Password
            };
            // FILTERING
            if (cmd.RoleId > 0)
            {
                query = from p in query where p.PositionRole == cmd.RoleId select p;
            }

            if (cmd.WorkPlaceId > 0)
            {
                query = from p in query where p.WorkPlace.Id == cmd.WorkPlaceId select p;
            }
            // SORTING
            if (cmd.SortByName != null)
            {
                query = cmd.SortByName == true
                    ? (from p in query orderby p.Name select p)
                    : (from p in query orderby p.Name descending select p);
            }

            if (cmd.SortBySurName != null)
            {
                query = cmd.SortBySurName == true
                    ? (from p in query orderby p.Surname select p)
                    : (from p in query orderby p.Surname descending select p);
            }

            if (cmd.SortByRoleId != null)
            {
                query = cmd.SortByRoleId == true
                    ? (from p in query orderby p.PositionRole select p)
                    : (from p in query orderby p.PositionRole descending select p);
            }

            if (cmd.SortByWorkPlaceId != null)
            {
                query = cmd.SortByWorkPlaceId == true
                    ? (from p in query orderby p.WorkPlace.Name select p)
                    : (from p in query orderby p.WorkPlace.Name descending select p);
            }

            pagesCount = (int) Math.Ceiling((double) (from p in query select p).Count()/cmd.PageSize);
            query = query.Page(cmd.PageSize,cmd.Page);

            return query.ToList();
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
                isSunnat = c.isSunnat,
                WorkPlace = new WorkPlaceDTO()
                {
                    Id = c.WorkPlace.Id,
                    Name = c.WorkPlace.Name,
                    Location = c.WorkPlace.Location,
                    IsDeleted = c.IsDeleted,
                },
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
            var element = new AccountUser(accountUser.Name, accountUser.Surname,accountUser.PositionRole,accountUser.PhoneNumber,accountUser.WorkPlace.Id.Value,accountUser.Password);
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
            accountUserToUpdate.WorkPlaceId = user.WorkPlace.Id.Value;
            accountUserToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void UpdatePassword(AccountUserDTO user)
        {
            var accountUserToUpdate = (from c in db.AccountUsers where c.Id == user.Id select c).FirstOrDefault();
            accountUserToUpdate.Password = user.Password;
            accountUserToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }
        public void Delete(int id)
        {
            var element = (from c in db.AccountUsers where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
