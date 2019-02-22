using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    public class ClientDalFacade
    {
        private LinqContext db = null;
        public ClientDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ClientDTO> GetClients()
        {
            var query = from c in db.Clients where c.IsDeleted == false
                select new ClientDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Address = c.Address,
                    Phone = c.Phone,
                    PaymentAccount = c.PaymentAccount,
                    BankDetails = c.BankDetails,
                    City = c.City,
                    MFO = c.MFO,
                    INN = c.INN,
                    OKONX = c.OKONX
                };
            return query.ToList();
        }

        public ClientDTO GetClient(int? id)
        {
            var query = from c in db.Clients where c.Id == id select new ClientDTO()
            {
                Id =  c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                PaymentAccount = c.PaymentAccount,
                BankDetails = c.BankDetails,
                City = c.City,
                MFO = c.MFO,
                INN = c.INN,
                OKONX = c.OKONX
            };
            return query.FirstOrDefault();
        }

        public void Add(ClientDTO client)
        {
            var element = new Client()
            {
                Name = client.Name,
                Address = client.Address,
                Phone = client.Phone,
                PaymentAccount = client.PaymentAccount,
                BankDetails = client.BankDetails,
                City = client.City,
                MFO = client.MFO,
                INN = client.INN,
                OKONX = client.OKONX
            };
            db.Clients.InsertOnSubmit(element);
            db.SubmitChanges();
        }

        public void Update(ClientDTO client)
        {
            var clientToUpdate = (from c in db.Clients where c.Id == client.Id select c).FirstOrDefault();
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
            var element = (from c in db.Clients where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
