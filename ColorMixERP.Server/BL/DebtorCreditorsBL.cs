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
            if (dto.IsDebtor)
            {
                dto.Amount = -1 * dto.Amount;
            }
            new DebtorCreditorsDalFacade().Add(dto);
            UpdateDebtorCreditorPart(dto);
        }

        public void Update(DebtorCreditorDTO dto)
        {
            if (dto.IsDebtor)
            {
                dto.Amount = -1 * dto.Amount;
            }
            new DebtorCreditorsDalFacade().Update(dto);
            var creditorDebtorExsting = new DebtorCreditorsDalFacade().GetDebtorCreditorById(dto.Id);
            if (dto.IsDebtor)
            {
                dto.Amount = creditorDebtorExsting.Amount - dto.Amount;
            }
            else
            {
                dto.Amount = dto.Amount - creditorDebtorExsting.Amount;
            }

            UpdateDebtorCreditorPart(dto);
        }

        public void UpdateDebtorCreditorPart(DebtorCreditorDTO dto)
        {
            var client = new ClientDalFacade().GetClient(dto.ClientId);
            var amount = client.DebtorCreditor + dto.Amount;
            new ClientDalFacade().UpdateDebtorCreditorPart(dto.ClientId, amount);
        }
    } 
}
