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
                Accountant = c.Accountant,
                Address = c.Address,
                BankDetails = c.BankDetails,
                City = c.City,
                Director = c.Director,
                INN = c.INN,
                LegalName = c.LegalName,
                MFO = c.MFO,
                PaymentAccount = c.PaymentAccount,
                Phone = c.Phone,
                OKONX = c.OKONX,
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
                Accountant = c.Accountant,
                Address = c.Address,
                BankDetails = c.BankDetails,
                City = c.City,
                Director = c.Director,
                INN = c.INN,
                LegalName = c.LegalName,
                MFO = c.MFO,
                PaymentAccount = c.PaymentAccount,
                Phone = c.Phone,
                OKONX = c.OKONX,
                ProductStocks = new ProductStockDalFacade().GetWorkPlaceProductStocks(c.Id ?? 0)
            };
            return result.FirstOrDefault();
        }

        public void Add(WorkPlaceDTO c)
        {
            var element = new WorkPlace()
            {
                Name = c.Name,
                Location = c.Location,
                Accountant = c.Accountant,
                Address = c.Address,
                BankDetails = c.BankDetails,
                City = c.City,
                Director = c.Director,
                INN = c.INN,
                LegalName = c.LegalName,
                Id = c.Id,
                MFO = c.MFO,
                PaymentAccount = c.PaymentAccount,
                Phone = c.Phone,
                OKONX = c.OKONX
            };
            element.ProductStock = new EntitySet<ProductStock>();
            db.WorkPlaces.InsertOnSubmit(element);
            db.SubmitChanges();
        }

        public void Update(WorkPlaceDTO workPlace)
        {
            var workplaceToUpdate = (from c in db.WorkPlaces where c.Id == workPlace.Id select c).FirstOrDefault();
            workplaceToUpdate.Location = workPlace.Location;
            workplaceToUpdate.Name = workPlace.Name;
            workplaceToUpdate.Accountant = workPlace.Accountant;
            workplaceToUpdate.Address = workPlace.Address;
            workplaceToUpdate.BankDetails = workPlace.BankDetails;
            workplaceToUpdate.City = workPlace.City;
            workplaceToUpdate.Director = workPlace.Director;
            workplaceToUpdate.INN = workPlace.INN;
            workplaceToUpdate.LegalName = workPlace.LegalName;
            workplaceToUpdate.Id = workPlace.Id;
            workplaceToUpdate.MFO = workPlace.MFO;
            workplaceToUpdate.PaymentAccount = workPlace.PaymentAccount;
            workplaceToUpdate.Phone = workPlace.Phone;
            workplaceToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var isSunnatWorkplaces = (from c in db.AccountUsers where c.isSunnat == true select c.WorkPlaceId).ToList();
            var element = (from c in db.WorkPlaces where c.Id == id select c).FirstOrDefault();
            var isNotContained = true;
            foreach (var workplaceId in isSunnatWorkplaces)
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
