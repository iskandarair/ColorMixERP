using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class SaleDalFacade
    {
        private LinqContext db;

        public SaleDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<SaleDTO> GetSales(SaleCommand cmd, ref int pagesCount)
        {
            var query = from c in db.Sales
                where c.IsDeleted == false
                select c;
            if (cmd.Currency > 0)
            {
                query = from c in query where c.Product.Currency == cmd.Currency select c;
            }
             var query2 = from c in query select new SaleDTO()
            {
                Id = c.Id,
                ProductId = c.Product.Id,
                ProductName = c.ProductName,
                Quantity = c.Quantity,
                ProductPrice = c.ProductPrice,
                SalesPrice = c.SalesPrice,
                CurrencyRate = c.CurrencyRate == null ? 0 : c.CurrencyRate,
                OrderId = c.OrderId
            };

            if (cmd.ProductId > 0)
            {
                query2 = from p in query2 where p.ProductId == cmd.ProductId select p;
            }

            if (cmd.SortByProduct != null)
            {
                query2 = cmd.SortByProduct == true
                    ? (from p in query2 orderby p.ProductId select p)
                    : (from p in query2 orderby p.ProductId descending select p);
            }

            if (cmd.SortBySalesPrice != null)
            {
                query2 = cmd.SortBySalesPrice == true
                    ? (from p in query2 orderby p.SalesPrice select p)
                    : (from p in query2 orderby p.SalesPrice descending select p);
            }

            if (cmd.SortByQuantity != null)
            {
                query2 = cmd.SortByQuantity == true
                    ? (from p in query2 orderby p.Quantity select p)
                    : (from p in query2 orderby p.Quantity descending select p);
            }

            pagesCount = (int)Math.Ceiling((double)(from p in query2 select p).Count()/cmd.PageSize);
            query2 = query2.Page(cmd.PageSize, cmd.Page);
            return query2.ToList();
        }
        public List<SaleDTO> GetOrderSale(int orderId)
        {
            var query = from c in db.Sales where c.OrderId == orderId select new SaleDTO()
                {
                    Id = c.Id,
                    ProductId = c.Product.Id,
                    ProductName = c.ProductName,
                    Quantity = c.Quantity,
                    ProductPrice = c.ProductPrice,
                    SalesPrice = c.SalesPrice,
                    CurrencyRate = c.CurrencyRate == null ? 0 : c.CurrencyRate,
                    OrderId = c.OrderId
                        };
            return query.ToList();
        }

        public SaleDTO GetSale(int id)
        {
            var query = from c in db.Sales where c.Id == id select new SaleDTO()
                {
                    Id = c.Id,
                    ProductId = c.Product.Id,
                    ProductName = c.ProductName,
                    Quantity = c.Quantity,
                    ProductPrice = c.ProductPrice,
                    SalesPrice = c.SalesPrice,
                    OrderId = c.OrderId,
                    CurrencyRate = c.CurrencyRate == null ? 0 : c.CurrencyRate,
            };
            return query.FirstOrDefault();
        }

        public decimal GetLatestCurrencyRate()
        {
            var query = (from c in db.Sales where c.CurrencyRate != null && c.CurrencyRate != 0 orderby c.Id descending select c)
                .FirstOrDefault();
            if (query != null)
            {
                return query.CurrencyRate.Value;
            }
            return 0;
        }
        public void Add(SaleDTO sale)
        {
            var element = new Sale(sale.ProductId)
            {
                ProductPrice = sale.ProductPrice,
                SalesPrice = sale.SalesPrice,
                Quantity = sale.Quantity,
                OrderId = sale.OrderId,
                CurrencyRate = sale.CurrencyRate
            };
            db.Sales.InsertOnSubmit(element);
            db.SubmitChanges();
        }
        public void AddRange(List<SaleDTO> sales, int orderId)
        {
            var result = new List<Sale>();
            foreach (var sale in sales)
            {
                result.Add(new Sale(sale.ProductId)
                {
                    ProductPrice = sale.ProductPrice,
                    SalesPrice = sale.SalesPrice,
                    Quantity =  sale.Quantity,
                    OrderId = orderId,
                    CurrencyRate = sale.CurrencyRate
                });
            }
            db.Sales.InsertAllOnSubmit(result);
            db.SubmitChanges();
        }

        public void Update(SaleDTO sale)
        {
            var query = from c in db.Sales where c.Id == sale.Id select c;
            var element = query.FirstOrDefault();
            element.Quantity = sale.Quantity;
            element.ProductPrice = sale.ProductPrice;
            element.SalesPrice = sale.SalesPrice;
            element.UpdatedDate = DateTime.Now;
            element.CurrencyRate = sale.CurrencyRate;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.Sales where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
