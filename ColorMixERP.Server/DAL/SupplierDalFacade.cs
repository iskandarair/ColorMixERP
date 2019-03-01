using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class SupplierDalFacade
    {
        private LinqContext db = null;
        public SupplierDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<SupplierDTO> GetSuppliers(PaginationDTO cmd, ref int pagesCount)
        {
            var query = from c in db.Suppliers where c.IsDeleted == false
                select new SupplierDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    SupplierInfo = c.SupplierInfo,
                };

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count()/cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public SupplierDTO GetSupplier(int? id)
        {
            var query = from c in db.Suppliers where c.Id == id select new SupplierDTO()
            {
                Id = c.Id,
                Name = c.Name,
                SupplierInfo = c.SupplierInfo,
            };
            return query.FirstOrDefault();
        }

        public void Add(SupplierDTO supplier)
        {
            var supplierToAdd = new Supplier()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                SupplierInfo = supplier.SupplierInfo,
            };
            db.Suppliers.InsertOnSubmit(supplierToAdd);
            db.SubmitChanges();
        }

        public void Update(SupplierDTO supplier)
        {
            var supplierToUpdate = (from c in db.Suppliers where c.Id == supplier.Id select c).FirstOrDefault();
            supplierToUpdate.Name = supplier.Name;
            supplierToUpdate.SupplierInfo = supplier.SupplierInfo;
            supplierToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.Suppliers where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
