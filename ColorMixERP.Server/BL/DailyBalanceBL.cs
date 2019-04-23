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
    public class DailyBalanceBL
    {
        public List<DailyBalanceDTO> GetDailyBalances(DailyBalanceCommand cmd,int userId, ref int pagesCount)
        {
            var userData = new UserBL().GetAccountUser(userId);
            bool isAdmin = userData.PositionRole == 1;

            return new DailyBalanceDalFacade().GetDailyBalancesStats(cmd, userData.WorkPlace.Id.Value, isAdmin, ref pagesCount);
        }
        public DailyBalanceDTO GetDailyBalance(int id)
        {
            return new DailyBalanceDalFacade().GetDailyBalance(id);
        }

        public void Add(DailyBalanceDTO dto)
        {
            new DailyBalanceDalFacade().Add(dto);
        }

        public void Add(List<DailyBalanceDTO> dtos)
        {
            new DailyBalanceDalFacade().Add(dtos);
        }

        public void Update(DailyBalanceDTO dto)
        {
            new DailyBalanceDalFacade().Update(dto);
        }

        public void Delete(int id)
        {
            new DailyBalanceDalFacade().Delete(id);
        }

        ////
        public void AuditDailyBalance()
        {
            var dtos = new ProductStockDalFacade().GetAllDailyBalanceDTO();
            Add(dtos);
        }
    }
}
