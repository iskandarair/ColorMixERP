using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class ProductBL
    {
        public List<ProductDTO> GetProducts(ProductCommand command)
        {
            if (string.IsNullOrEmpty(command.ProductCode))
            {
               command.ProductCode = string.Empty;
            }
            if (string.IsNullOrEmpty(command.ProductName))
            {
                command.ProductName = string.Empty;
            }
            if (string.IsNullOrEmpty(command.CategoryCode))
            {
                command.CategoryCode = string.Empty;
            }
            if (string.IsNullOrEmpty(command.CategoryName))
            {
                command.CategoryName = string.Empty;
            }
            if (string.IsNullOrEmpty(command.SupplierName))
            {
                command.SupplierName = string.Empty;
            }
            return new ProductDalFacade().GetProducts(command);
        }

        public ProductDTO GetProduct(int? id)
        {
            return new ProductDalFacade().GetProduct(id);
        }

        public void Add(ProductDTO product)
        {
            new ProductDalFacade().Add(product);
        }

        public void Update(ProductDTO product)
        {
            new ProductDalFacade().Update(product);
        }

        public void Delete(int id)
        {
            new ProductDalFacade().Delete(id);
        }
    }
}
