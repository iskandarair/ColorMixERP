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
    public class ProductDalFacade
    {
        private LinqContext db = null;
        public ProductDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<Product> GetProducts()
        {
            DataLoadOptions options = new DataLoadOptions();;
            options.LoadWith<Product>(i => i.Category);
            options.LoadWith<Product>(i => i.Supplier);
            db.LoadOptions = options;

            var query = from p in db.Products select p;
            return query.ToList();
        }

        public Product GetProduct(int? id)
        {
            DataLoadOptions options = new DataLoadOptions(); ;
            options.LoadWith<Product>(i => i.Category);
            options.LoadWith<Product>(i => i.Supplier);
            db.LoadOptions = options;

            var query = from p in db.Products where p.Id == id select p;
            return query.FirstOrDefault();
        }

        public void Add(Product product)
        {
            var ProductToAdd = new Product();;
            ProductToAdd = product;
            db.Products.InsertOnSubmit(ProductToAdd);
            db.SubmitChanges();
        }

        public void Update(Product product)
        {
            var productToUpdate = GetProduct(product.Id);
            productToUpdate.Code = product.Code;
            productToUpdate.BoxedNumber = product.BoxedNumber;
            productToUpdate.Currency = product.Currency;
            productToUpdate.MeasurementUnit = product.MeasurementUnit;
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Category = product.Category;
            productToUpdate.Supplier = product.Supplier;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var product = GetProduct(id);
            db.Products.DeleteOnSubmit(product);
            db.SubmitChanges();
        }
    }
}
