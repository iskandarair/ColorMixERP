using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using System.Data.Linq;
using System.Security.Principal;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class WorkPlaceDalFacade
    {
        private LinqContext db = null;
        public WorkPlaceDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<WorkPlaceDTO> GetWorkPlaces(PaginationDTO cmd, ref int pagesCount)
        { 
            var query = from c in db.WorkPlaces where c.IsDeleted == false
                select new WorkPlaceDTO()
            {
                Id =  c.Id,
                Name = c.Name,
                Location = c.Location,
                ProductStocks = new ProductStockDalFacade().GetWorkPlaceProductStocks(c.Id ?? 0)
            };
            pagesCount = (int) Math.Ceiling((double)(from c in query select c).Count() / cmd.PageSize); 
            query = query.Page(cmd.PageSize, cmd.Page);
            return  query.ToList();
        }

        public WorkPlaceDTO GetWorkPlace(int? id)
        { 
            var result = from c in db.WorkPlaces where c.Id == id select new WorkPlaceDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Location = c.Location,
                ProductStocks = new ProductStockDalFacade().GetWorkPlaceProductStocks(c.Id ?? 0)
            };
            return result.FirstOrDefault();
        }

        public void Add(WorkPlaceDTO workPlace)
        {
            var element = new WorkPlace()
            {
                Name = workPlace.Name,
                Location = workPlace.Location
            };
            element.ProductStock = new EntitySet<ProductStock>();
            db.WorkPlaces.InsertOnSubmit(element);
            db.SubmitChanges();
        }

        public void Update(WorkPlaceDTO workPlace)
        {
            var workplaceToUpdate = (from c in db.WorkPlaces where c.Id == workPlace.Id select c).FirstOrDefault();
            workplaceToUpdate.Location = workPlace.Location;
            workplaceToUpdate.Name = workplaceToUpdate.Name;
            workplaceToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var isSunatWorkplaces = (from c in db.AccountUsers where c.isSunnat == true select c.WorkPlaceId).ToList();
            var element = (from c in db.WorkPlaces where c.Id == id select c).FirstOrDefault();
            var isNotContained = true;
            foreach (var workplaceId in isSunatWorkplaces)
            {
                if (element.Id == workplaceId)
                {
                    isNotContained = false;
                }
            }
            // =    =       =   =       =   =   
            if (isNotContained)
            {
                element.IsDeleted = true;
                element.DeletedDate = DateTime.Now;
                db.SubmitChanges();
            }

        }
    }
}
