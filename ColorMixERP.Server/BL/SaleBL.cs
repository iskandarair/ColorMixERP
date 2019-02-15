using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class SaleBL
    {
        public List<SaleDTO> GetSales()
        {
            return new SaleDalFacade().GetSales();
        }

        public List<SaleDTO> GetOrderSale(int orderId)
        {
            return new SaleDalFacade().GetOrderSale(orderId);
        }

        public SaleDTO GetSale(int id)
        {
            return new SaleDalFacade().GetSale(id);
        }

        public void Add(SaleDTO sale)
        {
            new SaleDalFacade().Add(sale);
        }

        public void AddRange(List<SaleDTO> sales,int orderId)
        {
            new SaleDalFacade().AddRange(sales, orderId);
        }

        public void Update(SaleDTO sale)
        {
            new SaleDalFacade().Update(sale);
        }

        public void Delete(int id)
        {
            new SaleDalFacade().Delete(id);
        }
    }
}
