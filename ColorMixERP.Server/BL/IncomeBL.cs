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
    public class IncomeBL
    {
        public List<IncomeDTO> GetIncomes(IncomeCommand command, ref int pagesCount)
        {
            return new IncomeDalFacade().GetIncomes(command, ref pagesCount);
        }
        public IncomeDTO GetIncome(int id)
        {
            return new IncomeDalFacade().GetIncome(id);
        }
        public List<IncomeDTO> GetIncomes(int[] ids)
        {
            return new IncomeDalFacade().GetIncomes(ids);
        }
        public List<IncomeProductDTO> GetIncomeProducts(int incomeId)
        {
            return new IncomeDalFacade().GetIncomeProducts(incomeId);
        }
        public IncomeProductDTO GetIncomeProductById(int id)
        {
            return new IncomeDalFacade().GetIncomeProductById(id);
        }

        public void AddIncome(IncomeDTO dto)
        {
            new IncomeDalFacade().AddIncome(dto);
        }

        public void AddIncomeProducts(int incomeId, List<IncomeProductDTO> dtos)
        {
            new IncomeDalFacade().AddIncomeProducts(incomeId,dtos);
        }

        public void Update(IncomeProductDTO dto)
        {
            new IncomeDalFacade().Update(dto);
        }
    }
}
