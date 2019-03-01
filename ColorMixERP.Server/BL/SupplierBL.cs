using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class SupplierBL
    {
        public List<SupplierDTO> GetSuppliers(PaginationDTO cmd, ref int pagesCount)
        {
            return new SupplierDalFacade().GetSuppliers(cmd, ref pagesCount);
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
