using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using System.Data.Linq;
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

        public List<AccountUser> GetAccountUsers()
        {
            DataLoadOptions options = new DataLoadOptions();
            options.LoadWith<AccountUser>(ii => ii.Expenses);
            options.LoadWith<AccountUser>(ii => ii.WorkPlace);
            options.LoadWith<ProductStock>(ii => ii.Product);
            options.LoadWith<Product>(ii => ii.Supplier);
            options.LoadWith<Product>(ii => ii.Category);
            db.LoadOptions = options;
            var query =  from c in db.AccountUsers select c;
            var result = query.ToList();

            return result;
        }
        public AccountUser GetAccountUser(int? id)
        {
            DataLoadOptions options = new DataLoadOptions();
            options.LoadWith<AccountUser>(ii => ii.Expenses);
            options.LoadWith<AccountUser>(ii => ii.WorkPlace);
            options.LoadWith<ProductStock>(ii => ii.Product);
            options.LoadWith<Product>(ii => ii.Supplier);
            options.LoadWith<Product>(ii => ii.Category);
            db.LoadOptions = options;
            var query = from c in db.AccountUsers where c.Id == id select c;
            var result = query.FirstOrDefault();

            return result;
        }
        public User GetAccountByName(string name, string password)
        {
            DataLoadOptions options = new DataLoadOptions();
            options.LoadWith<AccountUser>(ii => ii.Expenses);
            options.LoadWith<AccountUser>(ii => ii.WorkPlace);
            options.LoadWith<ProductStock>(ii => ii.Product);
            options.LoadWith<Product>(ii => ii.Supplier);
            options.LoadWith<Product>(ii => ii.Category);
            db.LoadOptions = options;
            var query = from c in db.AccountUsers where c.Name == name && c.Password ==password  select new User(c.Id, c.Name);
            var result = query.FirstOrDefault();

            return result;
        }

        public void Add(AccountUser accountUser)
        {
            accountUser.Id = null;
            var element = new AccountUser();;
            element = accountUser;
            db.AccountUsers.InsertOnSubmit(element);
            db.SubmitChanges();
        }
        public void Update(AccountUser accountUser)
        {
            var accountUserToUpdate = GetAccountUser(accountUser.Id);
            accountUserToUpdate.Name = accountUser.Name;
            accountUserToUpdate.Surname = accountUser.Surname;
            accountUserToUpdate.PhoneNumber = accountUser.PhoneNumber;
            accountUserToUpdate.PositionRole = accountUser.PositionRole;
            db.SubmitChanges();
        }
        public void Delete(int id)
        {
            var element = GetAccountUser(id);
            db.AccountUsers.DeleteOnSubmit(element);
            db.SubmitChanges();
        }
    }
}
