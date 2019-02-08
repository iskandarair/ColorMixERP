using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;

namespace ColorMixERP.Server.BL
{
    public class SupplierBL
    {
        public List<Supplier> GetSuppliers()
        {
            return new SupplierDalFacade().GetSuppliers();
        }

        public Supplier GetSupplier(int? id)
        {
            return new SupplierDalFacade().GetSupplier(id);
        }

        public void Add(Supplier supplier)
        {
            new SupplierDalFacade().Add(supplier);
        }

        public void Update(Supplier supplier)
        {
            new SupplierDalFacade().Update(supplier);
        }

        public void Delete(int id)
        {
            new SupplierDalFacade().Delete(id);
        }
    }
}
