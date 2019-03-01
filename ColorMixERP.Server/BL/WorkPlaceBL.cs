
using ColorMixERP.Server.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class WorkPlaceBL
    {
        public List<WorkPlaceDTO> GetWorkPlaces(PaginationDTO cmd, ref int pagesCount)
        {
            return new WorkPlaceDalFacade().GetWorkPlaces(cmd, ref pagesCount);
        }

        public WorkPlaceDTO GetWorkPlace(int? id)
        {
            return new WorkPlaceDalFacade().GetWorkPlace(id);
        }

        public void Add(WorkPlaceDTO workPlace)
        {
            new WorkPlaceDalFacade().Add(workPlace);
        }
        public void Update(WorkPlaceDTO workPlace)
        {
            new WorkPlaceDalFacade().Update(workPlace);
        }
        public void Delete(int id)
        {
            new WorkPlaceDalFacade().Delete(id);
        }
    }
}
