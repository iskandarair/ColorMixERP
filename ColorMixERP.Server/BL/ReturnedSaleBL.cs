using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class ReturnedSaleBL
    {
        public List<ReturnedSaleDTO> GetReturnedSales(PaginationDTO cmd, ref int pagesCount)
        {
            return new ReturnedSaleDalFacade().GetReturnedSales(cmd, ref pagesCount);
        }

        public List<ReturnedSaleDTO> GetOrderReturnedSale(int orderId)
        {
            return new ReturnedSaleDalFacade().GetOrderReturnSale(orderId);
        }

        public ReturnedSaleDTO GetReturnedSale(int id)
        {
            return new ReturnedSaleDalFacade().GetReturnedSale(id);
        }

        public void Add(ReturnedSaleDTO dto)
        {
            new ReturnedSaleDalFacade().Add(dto);
        }

        public void Update(ReturnedSaleDTO dto)
        {
            new ReturnedSaleDalFacade().Update(dto);
        }

        public void Delete(int id)
        {
            new ReturnedSaleDalFacade().Delete(id);
        }
    }
}
