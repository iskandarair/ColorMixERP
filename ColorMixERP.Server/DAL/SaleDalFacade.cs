using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.DAL
{
    public class SaleDalFacade
    {
        private LinqContext db;

        public SaleDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<SaleDTO> GetSales()
        {
            var query = from c in db.Sales select new SaleDTO()
                {
                    Id = c.Id,
                    ProductId = c.Product.Id,
                    ProductName = c.ProductName,
                    Quantity = c.Quantity,
                    ProductPrice = c.ProductPrice,
                    SalesPrice = c.SalesPrice,
                    OrderId = c.OrderId
                };
            return query.ToList();
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
                    OrderId = c.OrderId
                 };
            return query.FirstOrDefault();
        }

        public void Add(SaleDTO sale)
        {
            var element = new Sale(sale.ProductId)
            {
                ProductPrice = sale.ProductPrice,
                SalesPrice = sale.SalesPrice,
                Quantity = sale.Quantity,
                OrderId = sale.OrderId
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
                    OrderId = orderId
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
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.Sales where c.Id == id select c).FirstOrDefault();
            db.Sales.DeleteOnSubmit(element);
            db.SubmitChanges();
        }
    }
}
