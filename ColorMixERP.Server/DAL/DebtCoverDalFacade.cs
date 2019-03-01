using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class DebtCoverDalFacade
    {
        private LinqContext db;

        public DebtCoverDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<DebtCoverDTO> GetDebtCovers(PaginationDTO cmd, ref int pagesCount)
        {
            var query = from c in db.DebtCovers
                where c.IsDeleted == false
                select new DebtCoverDTO()
                {
                    Id = c.Id,
                    CoverDate = c.CoverDate,
                    PaymentByCard = c.PaymentByCard,
                    PaymentByTransfer = c.PaymentByTransfer,
                    PaymentByCash = c.PaymentByCash,
                    OrderId = c.OrderId
                };

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public List<DebtCoverDTO> GetOrderDebtCovers(int orderId)
        {
            var query = from c in db.DebtCovers
                where c.IsDeleted == false && c.OrderId == orderId
                select new DebtCoverDTO()
                {
                    Id = c.Id,
                    CoverDate = c.CoverDate,
                    PaymentByCard = c.PaymentByCard,
                    PaymentByTransfer = c.PaymentByTransfer,
                    PaymentByCash = c.PaymentByCash,
                    OrderId = c.OrderId
                };
            return query.ToList();
        }

        public DebtCoverDTO GetDebtCover(int id)
        {

            var query = from c in db.DebtCovers
                where c.IsDeleted == false && c.Id == id
                select new DebtCoverDTO()
                {
                    Id = c.Id,
                    CoverDate = c.CoverDate,
                    PaymentByCard = c.PaymentByCard,
                    PaymentByTransfer = c.PaymentByTransfer,
                    PaymentByCash = c.PaymentByCash,
                    OrderId = c.OrderId
                };
            return query.FirstOrDefault();
        }

        public void Add(DebtCoverDTO element)
        {
            var cover = new DebtCover()
            {
                OrderId = element.OrderId,
                CoverDate = element.CoverDate,
                PaymentByCard = element.PaymentByCard,
                PaymentByTransfer = element.PaymentByTransfer,
                PaymentByCash = element.PaymentByCash
            };
            db.DebtCovers.InsertOnSubmit(cover);
            db.SubmitChanges();
        }

        public void Update(DebtCoverDTO element)
        {
            var elementToUpdate = (from c in db.DebtCovers where c.Id == element.Id select c).FirstOrDefault();
            elementToUpdate.CoverDate = element.CoverDate;
            elementToUpdate.PaymentByCard = element.PaymentByCard;
            elementToUpdate.PaymentByCash = element.PaymentByCash;
            elementToUpdate.PaymentByTransfer = element.PaymentByTransfer;
            elementToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.DebtCovers where c.Id == id select c).FirstOrDefault();
            element.UpdatedDate = DateTime.Now;
            element.IsDeleted = true;
            db.SubmitChanges();
        }
    }
}
