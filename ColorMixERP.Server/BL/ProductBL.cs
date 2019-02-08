using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.BL
{
    public class ProductBL
    {
        public List<Product> GetProducts()
        {
            return new ProductDalFacade().GetProducts();
        }

        public Product GetProduct(int? id)
        {
            return new ProductDalFacade().GetProduct(id);
        }

        public void Add(Product product)
        {
            new ProductDalFacade().Add(product);
        }

        public void Update(Product product)
        {
            new ProductDalFacade().Update(product);
        }

        public void Delete(int id)
        {
            new ProductDalFacade().Delete(id);
        }
    }
}
