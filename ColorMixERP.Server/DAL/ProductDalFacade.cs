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

        public List<ProductDTO> GetProducts(ProductCommand command, ref int pagesCount)
        {
            var query = from p in db.Products
                where p.IsDeleted == false
                select new ProductDTO()
                {
                    Id = p.Id,
                    Code = p.Code,
                    Name = p.Name,
                    Category = new CategoryDTO()
                    {
                        Id = p.CategoryId,
                        Code = p.Category.Code,
                        Name = p.Category.Name,
                        IsDeleted = p.Category.IsDeleted
                    },
                    Price = p.Price,
                    Currency = p.Currency,
                    MeasurementUnit = p.MeasurementUnit,
                    BoxedNumber = p.BoxedNumber,
                    Supplier = new SupplierDTO()
                    {
                        Id = p.SupplierId,
                        Name = p.Supplier.Name,
                        SupplierInfo = p.Supplier.SupplierInfo,
                        IsDeleted = p.Supplier.IsDeleted
                    }
                };
            // F I L T E R I N G
            if (command.ProductId > 0)
            {
                query = from p in query where p.Id == command.ProductId select p;
            }

            if (!string.IsNullOrWhiteSpace(command.ProductName))
            {
                query = from p in query where p.Name.Contains(command.ProductName) select p;
            }

            if (command.CategoryId > 0)
            {
                query = from p in query where p.Category.Id == command.CategoryId select p;
            }

            if (command.SupplierId > 0)
            {
                query = from p in query where p.Supplier.Id == command.SupplierId select p;
            }
            //// S O R T I N G
            if (command.SortByProductCode != null)
            query = command.SortByProductCode == true ? (from p in query orderby p.Code select p) : (from p in query orderby p.Code descending select p );
            
            if (command.SortByproductName != null)
                query = command.SortByproductName == true ? (from p in query orderby p.Name select p) : (from p in query orderby p.Name descending select p);

            if (command.SortByCategory != null)
                query = command.SortByCategory == true ? (from p in query orderby p.Category.Code select p) : (from p in query orderby p.Category.Code descending select p);

            if (command.SortBySupplier != null)
                query = command.SortBySupplier == true ? (from p in query orderby p.Supplier.Name select p) : (from p in query orderby p.Supplier.Name descending select p);

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count()/ command.PageSize);
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
                    Name = p.Category.Name,
                    IsDeleted = p.Category.IsDeleted
                },
                Price = p.Price,
                Currency = p.Currency,
                MeasurementUnit = p.MeasurementUnit,
                BoxedNumber = p.BoxedNumber,
                Supplier = new SupplierDTO()
                {
                    Id = p.SupplierId,
                    Name = p.Supplier.Name,
                    SupplierInfo = p.Supplier.SupplierInfo,
                    IsDeleted = p.Supplier.IsDeleted
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

        public void AddRange(List<ProductDTO> productsDtos)
        {
            var result = new List<Product>();

            foreach (var dto in productsDtos)
            {
                dto.Category.Id = new CategoryDalFacade().GetOrCreateCategoryId(dto.Category);
                dto.Supplier.Id = new SupplierDalFacade().GetOrCreateSupplierId(dto.Supplier);

                var product  = new Product(dto.Category.Id, dto.Supplier.Id)
                {
                    Code = dto.Code,
                    Name = dto.Name,
                    Price = dto.Price,
                    Currency = dto.Currency,
                    MeasurementUnit = dto.MeasurementUnit,
                    BoxedNumber = dto.BoxedNumber
                };
                result.Add(product);
            }
            db.Products.InsertAllOnSubmit(result);
            db.SubmitChanges();
        }
    }
}
