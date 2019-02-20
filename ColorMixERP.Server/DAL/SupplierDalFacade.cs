﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    public class SupplierDalFacade
    {
        private LinqContext db = null;
        public SupplierDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<SupplierDTO> GetSuppliers()
        {
            var query = from c in db.Suppliers where c.IsDeleted == false
                select new SupplierDTO()
                {
                    Id = c.Id,
                    Name = c.Name,
                    SupplierInfo = c.SupplierInfo,
                };
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
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.Suppliers where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            db.SubmitChanges();
        }
    }
}
