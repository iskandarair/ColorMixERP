using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class DebtorCreditorsDalFacade
    {
        private LinqContext db = null;
        public DebtorCreditorsDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<DebtorCreditorDTO> GetDebtorsCreditors(DCCommand cmd, ref int pagesCount)
        {

            var query = from c in db.DebtorCreditors
                select new DebtorCreditorDTO()
                {
                    Id = c.Id,
                    ClientId = c.ClientId,
                    Client = c.Client,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    Amount = c.Amount,
                    IsDebtor = c.isDebtor
                };

            if (cmd.ClientId != null)
            {
                query = from c in query where c.ClientId == cmd.ClientId select c;
            }

            if (!string.IsNullOrEmpty(cmd.ClientName))
            {
                query = from c in query where c.Client.Name.Contains(cmd.ClientName) select c;
            }

            if (cmd.IsDebtor != null)
            {
                query = from c in query where c.IsDebtor == cmd.IsDebtor select c;
            }

            if (cmd.UpdatedDate != null)
            {
                query = from c in query where c.UpdatedDate.Date >= cmd.UpdatedDate.Value.Date &&
                                              c.UpdatedDate.Date <= cmd.UpdatedDate.Value.Date
                        select c;
            }

            if (cmd.FromUpdatedDate != null && cmd.ToUpdatedDate != null)
            {
                query = from p in query
                    where p.UpdatedDate.Date >= cmd.FromUpdatedDate.Value.Date &&
                          p.UpdatedDate.Date <= cmd.ToUpdatedDate.Value.Date
                        select p;
            }

            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public DebtorCreditorDTO GetDebtorCreditorById(int id)
        {
            var query = from c in db.DebtorCreditors
                where c.Id == id
                select new DebtorCreditorDTO()
                {
                    Id = c.Id,
                    ClientId = c.ClientId,
                    Client = c.Client,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdatedDate,
                    Amount = c.Amount,
                    IsDebtor = c.isDebtor
                };
            return query.FirstOrDefault();
        }

        public void Add(DebtorCreditorDTO dto)
        {
            var debtorCreditor = new DebtorCreditor()
            {
                ClientId =  dto.ClientId.Value,
                Amount = dto.Amount,
                CreatedDate = dto.CreatedDate,
                UpdatedDate = dto.UpdatedDate,
                isDebtor =  dto.IsDebtor
            };
            db.DebtorCreditors.InsertOnSubmit(debtorCreditor);
            db.SubmitChanges();
        }
                                                                                                                
        public void Update(DebtorCreditorDTO dto)
        {
            var income = (from c in db.DebtorCreditors where c.Id == dto.Id select c).FirstOrDefault();
            income.Amount = dto.Amount;
            income.UpdatedDate = dto.UpdatedDate;
            income.isDebtor = dto.IsDebtor;
            
            db.SubmitChanges();
        }
    }
}
