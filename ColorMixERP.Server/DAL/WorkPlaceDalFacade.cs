using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using System.Data.Linq;
using System.Security.Principal;

namespace ColorMixERP.Server.DAL
{
    public class WorkPlaceDalFacade
    {
        private LinqContext db = null;
        public WorkPlaceDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<WorkPlace> GetWorkPlaces()
        { 
            var result = from c in db.WorkPlaces select c;
            return result.ToList();
        }

        public WorkPlace GetWorkPlace(int? id)
        { 
            var result = from c in db.WorkPlaces where c.Id == id select c;
            return result.FirstOrDefault();
        }

        public void Add(WorkPlace workPlace)
        {
            workPlace.Id = null;
            var element = new WorkPlace(); ;
            element = workPlace;
            db.WorkPlaces.InsertOnSubmit(element);
            db.SubmitChanges();
        }

        public void Update(WorkPlace workPlace)
        {
            var workplaceToUpdate = GetWorkPlace(workPlace.Id);
            workplaceToUpdate.Location = workPlace.Location;
            workplaceToUpdate.Name = workplaceToUpdate.Name;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = GetWorkPlace(id);
            db.WorkPlaces.DeleteOnSubmit(element);
            db.SubmitChanges();
        }
    }
}
