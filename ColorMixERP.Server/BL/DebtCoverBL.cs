using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class DebtCoverBL
    {
        public List<DebtCoverDTO> GetDebtCovers()
        {
            return new DebtCoverDalFacade().GetDebtCovers();
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
