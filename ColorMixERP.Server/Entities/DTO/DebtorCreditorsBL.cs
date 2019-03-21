using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.Entities.DTO
{
    public class DebtorCreditorsBL
    {
        public List<DebtorCreditorDTO> Get(DCCommand cmd, ref  int pagesCount)
        {
            return new DebtorCreditorsDalFacade().GetDebtorsCreditors(cmd, ref pagesCount);
        }

        public DebtorCreditorDTO GetById(int id)
        {
            return new DebtorCreditorsDalFacade().GetDebtorCreditorById(id);
        }

        public void Add(DebtorCreditorDTO dto)
        {
            new DebtorCreditorsDalFacade().Add(dto);
        }

        public void Update(DebtorCreditorDTO dto)
        {
            new DebtorCreditorsDalFacade().Update(dto);
        }
    }
}
