using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class SupplierBL
    {
        public List<SupplierDTO> GetSuppliers()
        {
            return new SupplierDalFacade().GetSuppliers();
        }

        public SupplierDTO GetSupplier(int? id)
        {
            return new SupplierDalFacade().GetSupplier(id);
        }

        public void Add(SupplierDTO supplier)
        {
            new SupplierDalFacade().Add(supplier);
        }

        public void Update(SupplierDTO supplier)
        {
            new SupplierDalFacade().Update(supplier);
        }

        public void Delete(int id)
        {
            new SupplierDalFacade().Delete(id);
        }
    }
}
