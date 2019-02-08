using System;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Config;

namespace ColorMixERP.Server.DAL
{
    class ProductStockDalFacade
    {
        private LinqContext db = null;
        public ProductStockDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<ProductStock> GetProductStocks()
        {
            DataLoadOptions dl = new DataLoadOptions();
            dl.LoadWith<ProductStock>( p => p.Product);
            db.LoadOptions = dl;
            var query = from p in db.ProductStocks select p;
            return query.ToList();

        }
        
        public ProductStock GetProductStock(int? id)
        {
            DataLoadOptions dl = new DataLoadOptions();
            dl.LoadWith<ProductStock>(p => p.Product);
            db.LoadOptions = dl;
            var query = from p in db.ProductStocks where p.Id == id select p;
            return query.FirstOrDefault();
        }

        public void Add(int workPlaceId, ProductStock stock)
        {
            var workplace = new WorkPlaceDalFacade().GetWorkPlace(workPlaceId);
            workplace.ProductStock.Add(stock);
           //var productToAdd = new ProductStock(stock.Product.Id, workPlaceId);
           //productToAdd.Product = stock.Product;
           //productToAdd.Quantity = stock.Quantity;
           //db.ProductStocks.InsertOnSubmit(productToAdd);
            db.SubmitChanges(); 
        }

        public void Update(ProductStock stock)
        {
            var stockToUpdate = GetProductStock(stock.Id);
            stockToUpdate.Product = stock.Product;
            stockToUpdate.Quantity = stock.Quantity;
            db.SubmitChanges();
        }

        public void Delete(int? id)
        {
            var stock = GetProductStock(id);
            db.ProductStocks.DeleteOnSubmit(stock);
            db.SubmitChanges();
        }

        public List<ProductStock> GetWorkPlaceProductStocks(int wpId)
        {

            DataLoadOptions dl = new DataLoadOptions();
            dl.LoadWith<ProductStock>(p => p.Product);
            db.LoadOptions = dl;
            var query = from p in db.WorkPlaces where p.Id == wpId select p.ProductStock;
            return query.FirstOrDefault().ToList();
        }
    }
}
