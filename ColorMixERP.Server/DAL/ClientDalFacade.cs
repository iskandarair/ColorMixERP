using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class ClientDalFacade
    {
        private LinqContext db = null;
        public ClientDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ClientDTO> GetClients(ClientCommand cmd, ref int pagesCount)
        {
            var query = from c in db.Clients
                where c.IsDeleted == false
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
                    OKONX = c.OKONX,
                    NickName = c.NickName,
                    DebtorCreditor = c.DebtorCreditor,
                    WorkPlaceId = c.WorkPlaceId,
                    WorkPlaceName = c.WorkPlace.Name,
                };

            if (!string.IsNullOrEmpty(cmd.Name))
            {
                query = from c in query where c.Name.Contains(cmd.Name) select c;
            }

            if (!string.IsNullOrEmpty(cmd.NickName))
            {
                query = from c in query where c.NickName.Contains(cmd.NickName) select c;
            }

            if (!string.IsNullOrEmpty(cmd.INN))
            {
                query = from c in query where c.INN.Contains(cmd.INN) select c;
            }

            if (!string.IsNullOrEmpty(cmd.City))
            {
                query = from c in query where c.City.Contains(cmd.City) select c;
            }
            //WorkPlaceId
            if (cmd.WorkPlaceId > 0)
            {
                query = from c in query where c.WorkPlaceId == cmd.WorkPlaceId.Value select c;
            }
            //S O R T I N G
            if (cmd.SortByName != null)
            {
                query = cmd.SortByName == true
                    ? (from p in query orderby p.Name select p)
                    : (from p in query orderby p.Name descending select p);
            }

            if (cmd.SortByNickName != null)
            {
                query = cmd.SortByNickName == true
                    ? (from p in query orderby p.NickName select p)
                    : (from p in query orderby p.NickName descending select p);
            }

            if (cmd.SortByCity != null)
            {
                query = cmd.SortByCity == true
                    ? (from p in query orderby p.City select p)
                    : (from p in query orderby p.City descending select p);
            }

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
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
                OKONX = c.OKONX,
                NickName = c.NickName,
                DebtorCreditor = c.DebtorCreditor,
                WorkPlaceId = c.WorkPlaceId,
                WorkPlaceName = c.WorkPlace.Name,
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
                OKONX = client.OKONX,
                NickName = client.NickName,
                WorkPlaceId = client.WorkPlaceId,
            };
            db.Clients.InsertOnSubmit(element);
            db.SubmitChanges();
        }

        public void Update(ClientDTO client)
        {
            var clientToUpdate = (from c in db.Clients where c.Id == client.Id select c).FirstOrDefault();
            clientToUpdate.Name = client.Name;
            clientToUpdate.NickName = client.NickName;
            clientToUpdate.Address = client.Address;
            clientToUpdate.Phone = client.Phone;
            clientToUpdate.PaymentAccount = client.PaymentAccount;
            clientToUpdate.BankDetails = client.BankDetails;
            clientToUpdate.City = client.City;
            clientToUpdate.MFO = client.MFO;
            clientToUpdate.INN = client.INN;
            clientToUpdate.OKONX = client.OKONX;
            clientToUpdate.WorkPlaceId = client.WorkPlaceId;
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

        public void UpdateDebtorCreditorPart(int id, decimal debtorCreditor)
        {
            var element = (from c in db.Clients where c.Id == id select c).FirstOrDefault();
            element.DebtorCreditor = debtorCreditor;
            db.SubmitChanges();
        }
    }
}
