using System;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    class ProductStockDalFacade
    {
        private LinqContext db = null;
        public ProductStockDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<ProductStockDTO> GetProductStocks()
        {
            var query = from p in db.ProductStocks where p.IsDeleted == false
                select new ProductStockDTO()
            {
                Id = p.Id,
                ProductId = p.Product.Id,
                ProductName = p.Product.Name,
                Quantity = p.Quantity
            };
            return query.ToList();

        }
        
        public ProductStockDTO GetProductStock(int? id)
        {
            var query = from p in db.ProductStocks where p.Id == id
                select new ProductStockDTO()
                {
                    Id = p.Id,
                    ProductId = p.Product.Id,
                    ProductName = p.Product.Name,
                    Quantity = p.Quantity
                };
            return query.FirstOrDefault();
        }

        public void Add(int workPlaceId, ProductStockDTO stock)
        {
            var productToAdd = new ProductStock(stock.ProductId, workPlaceId);
            productToAdd.Quantity = stock.Quantity;
            db.ProductStocks.InsertOnSubmit(productToAdd);
            db.SubmitChanges();
        }

        public void Update(ProductStockDTO stock)
        {
            var stockToUpdate = (from p in db.ProductStocks where p.Id == stock.Id select p).FirstOrDefault() ;
            stockToUpdate.Quantity = stock.Quantity;
            stockToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int? id)
        {
            var element = (from c in db.ProductStocks where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public List<ProductStockDTO> GetWorkPlaceProductStocks(int wpId)
        {
            var query = from p in db.WorkPlaces where p.Id == wpId select p.ProductStock;
            var productStock = query.FirstOrDefault().ToList();
            var result = new List<ProductStockDTO>();
            foreach (var stock in productStock)
            {
                result.Add(new ProductStockDTO()
                {
                    Id = stock.Id,
                    ProductId = stock.Product.Id,
                    ProductName = stock.Product.Name,
                    Quantity =  stock.Quantity
                });
            }
            return result;
        }
    }
}
