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
    public class ProductDalFacade
    {
        private LinqContext db = null;
        public ProductDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ProductDTO> GetProducts()
        { 
            var query = from p in db.Products select new ProductDTO()
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Currency = p.Currency,
                MeasurementUnit = p.MeasurementUnit,
                BoxedNumber = p.BoxedNumber,
                Supplier = p.Supplier
            };
            return query.ToList();
        }

        public ProductDTO GetProduct(int? id)
        { 
            var query = from p in db.Products where p.Id == id select new ProductDTO()
            { 
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Category = p.Category,
                Price = p.Price,
                Currency = p.Currency,
                MeasurementUnit = p.MeasurementUnit,
                BoxedNumber = p.BoxedNumber,
                Supplier = p.Supplier
            }; ;
            return query.FirstOrDefault();
        }

        public void Add(ProductDTO product)
        {
            var productToAdd = new Product(product.Category.Id ?? 0, product.Supplier.Id)
            {
                Code = product.Code,
                Name = product.Name,
                Price = product.Price,
                Currency = product.Currency,
                MeasurementUnit = product.MeasurementUnit,
                BoxedNumber = product.BoxedNumber
            };
            db.Products.InsertOnSubmit(productToAdd);
            db.SubmitChanges();
        }

        public void Update(ProductDTO product)
        {
            var productToUpdate = (from p in db.Products where p.Id == product.Id select p).FirstOrDefault();
            productToUpdate.Code = product.Code;
            productToUpdate.BoxedNumber = product.BoxedNumber;
            productToUpdate.Currency = product.Currency;
            productToUpdate.MeasurementUnit = product.MeasurementUnit;
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.CategoryId = product.Category.Id ?? 0;
            productToUpdate.SupplierId = product.Supplier.Id;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var product = (from p in db.Products where p.Id == id select p).FirstOrDefault();
            db.Products.DeleteOnSubmit(product);
            db.SubmitChanges();
        }
    }
}
