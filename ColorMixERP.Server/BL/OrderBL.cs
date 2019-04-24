using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class OrderBL
    {
        public List<OrderDTO> GetClientOrders(OrderCommand cmd, int userId, ref int pagesCount)
        {
            var user = new UserDalFacade().GetAccountUser(userId);
            var isAdmin = user.isSunnat || user.PositionRole == 1;
            return new OrderDalFacade().GetClientOrders(cmd, user.WorkPlace.Id.Value, isAdmin, ref pagesCount);
        }
        public ClientOrderStatisticalDTO GetClientOrdersStatistics(OrderCommand cmd, int userId, ref int pagesCount)
        {
            var user = new UserDalFacade().GetAccountUser(userId);
            var isAdmin = user.isSunnat || user.PositionRole == 1;
            return new OrderDalFacade().GetClientOrdersStats(cmd, user.WorkPlace.Id.Value, isAdmin, ref pagesCount);
        }

        public OrderDTO GetClientOrder(int id)
        {
            return new OrderDalFacade().GetClientOrder(id);
        }

        public void Add(OrderDTO order)
        {
            if (order.ClientId == 0)
            {
                order.ClientId = null;
            }
            new ProductStockBL().UpdateProductStocks(order);
            new OrderDalFacade().Add(order);
            //
            var client = new ClientDalFacade().GetClient(order.ClientId);
            // in order to calculate client debt, we substract all the paid money from the order's overall price
            var debtAmount = -1 * (order.OverallPrice - (order.PaymentByTransfer + order.PaymentByCard + order.PaymentByCash));
            if(debtAmount == 0) return;

            var creditorDebtor = new DebtorCreditorDTO()
            {
                ClientId = order.ClientId,
                ///!!! very Important initially it was only paymentByTransfer, by illogical request all payment types are addded to count debt
                Amount =  debtAmount
            };
            new DebtorCreditorsBL().UpdateDebtorCreditorPart(creditorDebtor);
        }

        public void Update(OrderDTO order)
        {
            new OrderDalFacade().Update(order);
            var orderExisting = new OrderDalFacade().GetClientOrder(order.Id);
            var amount = orderExisting.PaymentByTransfer - order.PaymentByTransfer;
            var sales = new SaleBL().GetOrderSale(order.Id);
            sales.RemoveAll(x => order.Sales.Exists(y => y.Id == x.Id));
            foreach (var deletedSale in sales)
            {
                new SaleBL().Delete(deletedSale.Id);
            }
            var debtorCreditor = new DebtorCreditorDTO()
            {
                ClientId = order.ClientId,
                Amount = amount
            };
            new DebtorCreditorsBL().UpdateDebtorCreditorPart(debtorCreditor);
        }

        public void Delete(int id)
        {
            new OrderDalFacade().Delete(id);
        }
    }
}
