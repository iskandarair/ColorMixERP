
using ColorMixERP.Server.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;

namespace ColorMixERP.Server.BL
{
    public class WorkPlaceBL
    {
        public List<WorkPlaceDTO> GetWorkPlaces()
        {
            return new WorkPlaceDalFacade().GetWorkPlaces();
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
