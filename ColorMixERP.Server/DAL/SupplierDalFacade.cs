using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.DAL
{
    public class SupplierDalFacade
    {
        private LinqContext db = null;
        public SupplierDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<Supplier> GetSuppliers()
        {
            var query = from c in db.Suppliers select c;
            return query.ToList();
        }

        public Supplier GetSupplier(int? id)
        {
            var query = from c in db.Suppliers where c.Id == id select c;
            return query.FirstOrDefault();
        }

        public void Add(Supplier supplier)
        {
            var supplierToAdd = new Supplier();
            supplierToAdd = supplier;
            db.Suppliers.InsertOnSubmit(supplierToAdd);
            db.SubmitChanges();
        }

        public void Update(Supplier supplier)
        {
            var supplierToUpdate = GetSupplier(supplier.Id);
            supplierToUpdate.Name = supplier.Name;
            supplierToUpdate.SupplierInfo = supplier.SupplierInfo;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var supplier = GetSupplier(id);
            db.Suppliers.DeleteOnSubmit(supplier);
            db.SubmitChanges();
        }
    }
}
