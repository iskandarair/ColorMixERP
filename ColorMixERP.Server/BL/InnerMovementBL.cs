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
    public class InnerMovementBL
    {
        public List<InnerMovement> GetInnerMovements()
        {
            return new InnerMovementDalFacade().GetInnerMovements();
        }
        public List<InnerMovementDTO> GetInnerMovementDtos(InnerMovementCommand cmd, ref int pagesCount)
        {
            return new InnerMovementDalFacade().GetInnerMovementDtos(cmd, ref  pagesCount);
        }
        public InnerMovement GetInnerMovement(int id)
        {
            return  new InnerMovementDalFacade().GetInnerMovement(id);
        }
        public InnerMovementDTO GetInnerMovementDto(int id)
        {
            return new InnerMovementDalFacade().GetInnerMovementDto(id);
        }
        public void Add(InnerMovementDTO dto)
        {
            new InnerMovementDalFacade().Add(dto);
        }
        public void Update(InnerMovementDTO dto)
        {
            new InnerMovementDalFacade().Update(dto);
        }
        public void Delete(int id)
        {
            new InnerMovementDalFacade().Delete(id);
        }
    }
}
