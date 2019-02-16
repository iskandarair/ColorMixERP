using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    public class OrderDalFacade
    {
        private LinqContext db = null;
        public OrderDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<OrderDTO> GetClientOrders()
        {
            var query = from c in db.ClientOrders select new OrderDTO()
            {
                Id =  c.Id,
                TransactionId = c.TransactinoId,
                SalerId =  c.Saler.Id ?? 0,
                SalerName = c.Saler.Name + " " + c.Saler.Surname,
                OrderDate = c.OrderDate,
                Sales = new SaleBL().GetOrderSale(c.Id),
                ClientId = c.Client.Id ?? 0,
                ClientName = c.Client.Name,
                ClientRepresentitive = c.ClientRepresentitive,
                OverallPrice = c.OverallPrice,
                PaymentByCard = c.PaymentByCard,
                PaymentByTransfer = c.PaymentByTransfer,
                PaymentByCash = c.PaymentByCash,
                IsDebt = c.IsDebt
            };
            return query.ToList();
        }
        public OrderDTO GetClientOrder(int id)
        {
            var query = from c in db.ClientOrders where c.Id == id select new OrderDTO()
                {
                    Id = c.Id,
                    TransactionId = c.TransactinoId,
                    SalerId = c.Saler.Id ?? 0,
                    SalerName = c.Saler.Name + " " + c.Saler.Surname,
                    OrderDate = c.OrderDate,
                    Sales = new SaleBL().GetOrderSale(c.Id),
                    ClientId = c.Client.Id ?? 0,
                    ClientName = c.Client.Name,
                    ClientRepresentitive = c.ClientRepresentitive,
                    OverallPrice = c.OverallPrice,
                    PaymentByCard = c.PaymentByCard,
                    PaymentByTransfer = c.PaymentByTransfer,
                    PaymentByCash = c.PaymentByCash,
                    IsDebt = c.IsDebt
                };
            return query.FirstOrDefault();
        }

        public void Add(OrderDTO order)
        {
            var element = new ClientOrder(order.SalerId, order.ClientId)
            {
                TransactinoId = order.TransactionId,
                OrderDate = order.OrderDate,
                PaymentByCard = order.PaymentByCard,
                PaymentByTransfer = order.PaymentByTransfer,
                PaymentByCash = order.PaymentByCash,
                IsDebt = order.IsDebt,
                ClientRepresentitive = order.ClientRepresentitive,
                OverallPrice = order.OverallPrice
            };
            db.ClientOrders.InsertOnSubmit(element);
            db.SubmitChanges();
            new SaleBL().AddRange(order.Sales,element.Id);
        }

        public void Update(OrderDTO order)
        {
            var element = (from c in db.ClientOrders where c.Id == order.Id select c).FirstOrDefault();
            element.TransactinoId = order.TransactionId;
            element.OrderDate = order.OrderDate;
            element.PaymentByCard = order.PaymentByCard;
            element.PaymentByTransfer = order.PaymentByTransfer;
            element.PaymentByCash = order.PaymentByCash;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.ClientOrders where c.Id == id select c).FirstOrDefault();
            db.ClientOrders.DeleteOnSubmit(element);
            db.SubmitChanges();
        }
    }
}
