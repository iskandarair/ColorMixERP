﻿using System;
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
    class ProductStockDalFacade
    {
        private LinqContext db = null;
        public ProductStockDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<ProductStockDTO> GetProductStocks(ProductStockCommand cmd, ref int pagesCount)
        {
            var query = from p in db.ProductStocks where p.IsDeleted == false select p;

            if (cmd.ProductId > 0)
            {
                query = from p in query where p.ProductId == cmd.ProductId select p;
            }

            if (cmd.WorkPlaceId > 0)
            {
                query = from p in query where p.WorkPlaceId == cmd.WorkPlaceId select p;
            }

            if (cmd.SortByWorkPlaceId != null)
            {
                query = cmd.SortByWorkPlaceId == true
                    ? (from p in query orderby p.WorkPlaceId select p)
                    : (from p in query orderby p.WorkPlaceId descending select p);
            }

            if (cmd.SortByQuantity != null)
            {
                query = cmd.SortByQuantity == true
                    ? (from p in query orderby p.Quantity select p)
                    : (from p in query orderby p.Quantity descending select p);
            }

            var query2 = from p in query select new ProductStockDTO()
            {
                Id = p.Id,
                ProductId = p.Product.Id,
                ProductName = p.Product.Name,
                Quantity = p.Quantity
            };

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count()/cmd.PageSize);
            query2 = query2.Page(cmd.PageSize, cmd.Page);
            return query2.ToList();

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
