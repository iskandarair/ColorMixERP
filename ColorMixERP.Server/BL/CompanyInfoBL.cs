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
    public class CompanyInfoBL
    {
        public List<CompanyInfoDTO> GetObjects(PaginationDTO cmd, ref int pagesCount)
        {
            return new CompanyInfoDalFacade().GetCompanyInfos(cmd, ref pagesCount);
        }

        public CompanyInfoDTO GetObject(int id)
        {
            return new CompanyInfoDalFacade().GetCompanyInfo(id);
        }
        
        public void Add(CompanyInfoDTO dto)
        {
            new CompanyInfoDalFacade().Add(dto);
        }
        
        public void Update(CompanyInfoDTO dto)
        {
            new CompanyInfoDalFacade().Update(dto);
        }

        public void Delete(int id)
        {
            new CompanyInfoDalFacade().Delete(id);
        }
    }
}
