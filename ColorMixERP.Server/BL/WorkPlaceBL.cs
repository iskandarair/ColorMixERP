
using ColorMixERP.Server.Entities;
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
        public List<WorkPlace> GetWorkPlaces()
        {
            return new WorkPlaceDalFacade().GetWorkPlaces();
        }

        public WorkPlace GetWorkPlace(int? id)
        {
            return new WorkPlaceDalFacade().GetWorkPlace(id);
        }

        public void Add(WorkPlace workPlace)
        {
            new WorkPlaceDalFacade().Add(workPlace);
        }
        public void Update(WorkPlace workPlace)
        {
            new WorkPlaceDalFacade().Update(workPlace);
        }
        public void Delete(int id)
        {
            new WorkPlaceDalFacade().Delete(id);
        }
    }
}
