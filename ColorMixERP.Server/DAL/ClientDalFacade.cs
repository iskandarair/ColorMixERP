using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.DAL
{
    public class ClientDalFacade
    {
        private LinqContext db = null;
        public ClientDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<Client> GetClients()
        {
            var query = from c in db.Clients where c.IsDeleted == false select c;
            return query.ToList();
        }

        public Client GetClient(int? id)
        {
            var query = from c in db.Clients where c.Id == id select c;
            return query.FirstOrDefault();
        }

        public void Add(Client client)
        {
            db.Clients.InsertOnSubmit(client);
            db.SubmitChanges();
        }

        public void Update(Client client)
        {
            var clientToUpdate = GetClient(client.Id);
            clientToUpdate.Name = client.Name;
            clientToUpdate.Address = client.Address;
            clientToUpdate.Phone = client.Phone;
            clientToUpdate.PaymentAccount = client.PaymentAccount;
            clientToUpdate.BankDetails = client.BankDetails;
            clientToUpdate.City = client.City;
            clientToUpdate.MFO = client.MFO;
            clientToUpdate.INN = client.INN;
            clientToUpdate.OKONX = client.OKONX;
            clientToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.ClientOrders where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
