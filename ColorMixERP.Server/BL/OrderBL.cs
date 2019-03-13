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
        public List<OrderDTO> GetClientOrders(OrderCommand cmd, ref int pagesCount)
        {
            return new OrderDalFacade().GetClientOrders(cmd, ref pagesCount);
        }

        public OrderDTO GetClientOrder(int id)
        {
            return new OrderDalFacade().GetClientOrder(id);
        }

        public void Add(OrderDTO order)
        {
            new ProductStockBL().UpdateProductStocks(order);
            new OrderDalFacade().Add(order);
        }

        public void Update(OrderDTO order)
        {
            new OrderDalFacade().Update(order);
        }

        public void Delete(int id)
        {
            new OrderDalFacade().Delete(id);
        }
    }
}
