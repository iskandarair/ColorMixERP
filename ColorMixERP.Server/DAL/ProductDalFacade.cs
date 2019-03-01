using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class ProductDalFacade
    {
        private LinqContext db = null;
        public ProductDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ProductDTO> GetProducts(ProductCommand command)
        {
            var query = from p in db.Products
                where p.IsDeleted == false &&
                      p.Code.Contains(command.ProductCode) &&
                      p.Name.Contains(command.ProductName) &&
                      p.Category.Code.Contains(command.CategoryCode) &&
                      p.Category.Name.Contains(command.CategoryName) &&
                      p.Supplier.Name.Contains(command.SupplierName)
                select new ProductDTO()
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Category = new CategoryDTO()
                    {
                        Id = p.CategoryId,
                        Code = p.Category.Code,
                        Name = p.Category.Name
                    },
                    Price = p.Price,
                    Currency = p.Currency,
                    MeasurementUnit = p.MeasurementUnit,
                    BoxedNumber = p.BoxedNumber,
                    Supplier = new SupplierDTO()
                    {
                        Id = p.SupplierId,
                        Name = p.Supplier.Name,
                        SupplierInfo = p.Supplier.SupplierInfo
                    }
                };

            if(command.SortByProductCode != null)
            query = command.SortByProductCode == true ? (from p in query orderby p.Code select p) : (from p in query orderby p.Code descending select p );
            
            if (command.SortByproductName != null)
                query = command.SortByproductName == true ? (from p in query orderby p.Name select p) : (from p in query orderby p.Name descending select p);

            if (command.SortByCategory != null)
                query = command.SortByCategory == true ? (from p in query orderby p.Category.Code select p) : (from p in query orderby p.Category.Code descending select p);

            if (command.SortBySupplier != null)
                query = command.SortBySupplier == true ? (from p in query orderby p.Supplier.Name select p) : (from p in query orderby p.Supplier.Name descending select p);

            query = query.Page(command.PageSize, command.Page);

            return query.ToList();
        }

        public ProductDTO GetProduct(int? id)
        { 
            var query = from p in db.Products where p.Id == id select new ProductDTO()
            { 
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Category = new CategoryDTO()
                {
                    Id = p.CategoryId,
                    Code = p.Category.Code,
                    Name = p.Category.Name
                },
                Price = p.Price,
                Currency = p.Currency,
                MeasurementUnit = p.MeasurementUnit,
                BoxedNumber = p.BoxedNumber,
                Supplier = new SupplierDTO()
                {
                    Id = p.SupplierId,
                    Name = p.Supplier.Name,
                    SupplierInfo = p.Supplier.SupplierInfo
                }
            }; ;
            return query.FirstOrDefault();
        }

        public void Add(ProductDTO product)
        {
            var productToAdd = new Product(product.Category.Id, product.Supplier.Id)
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
            productToUpdate.CategoryId = product.Category.Id;
            productToUpdate.SupplierId = product.Supplier.Id;
            productToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.Products where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
