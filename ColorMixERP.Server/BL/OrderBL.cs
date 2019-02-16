using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class OrderBL
    {
        public List<OrderDTO> GetClientOrders()
        {
            return new OrderDalFacade().GetClientOrders();
        }

        public OrderDTO GetClientOrder(int id)
        {
            return new OrderDalFacade().GetClientOrder(id);
        }

        public void Add(OrderDTO order)
        {
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
