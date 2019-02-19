using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class ProductBL
    {
        public List<ProductDTO> GetProducts()
        {
            return new ProductDalFacade().GetProducts();
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
