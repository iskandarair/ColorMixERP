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
    public class DebtCoverBL
    {
        public List<DebtCoverDTO> GetDebtCovers(PaginationDTO cmd, ref int pagesCount)
        {
            return new DebtCoverDalFacade().GetDebtCovers(cmd, ref pagesCount);
        }

        public List<DebtCoverDTO> GetOrderDebtCovers(int orderId)
        {
            return new DebtCoverDalFacade().GetOrderDebtCovers(orderId);
        }

        public DebtCoverDTO GetDebtCover(int id)
        {
            return new DebtCoverDalFacade().GetDebtCover(id);
        }

        public void Add(DebtCoverDTO dto)
        {
            new DebtCoverDalFacade().Add(dto);
        }

        public void Update(DebtCoverDTO dto)
        {
            new DebtCoverDalFacade().Update(dto);
        }

        public void Delete(int id)
        {
            new DebtCoverDalFacade().Delete(id);
        }
    }
}
