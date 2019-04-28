using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class ReturnedSaleDalFacade
    {
        private LinqContext db;

        public ReturnedSaleDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ReturnedSaleDTO> GetReturnedSales(PaginationDTO cmd, int workplaceId, bool isAdmin, ref int pagesCount)
        {
            IQueryable<ReturnedSaleDTO> query = from c in db.ReturnedSales
                join sale in db.Sales on c.SaleId equals sale.Id
                join order in db.ClientOrders on sale.OrderId equals order.Id
                join user in db.AccountUsers on order.SalerId equals user.Id
                where c.IsDeleted == false && user.WorkPlaceId == workplaceId
                select new ReturnedSaleDTO()
                {
                    Id = c.Id,
                    SaleId = c.SaleId,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Cause = c.Cause,
                    DefectedQuantity = c.DefectedQuantity,
                    Quantity = c.Quantity,
                    ReturnDate = c.ReturnDate,
                    ReturnedPrice = c.ReturnedPrice,
                    ReturnedMoney = c.ReturnedMoney
                };

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public List<ReturnedSaleDTO> GetOrderReturnSale(int orderId)
        {
            var query = from c in db.ReturnedSales
                        where c.IsDeleted == false && c.Sale.OrderId == orderId
                        select new ReturnedSaleDTO()
                        {
                            Id = c.Id,
                            SaleId = c.Sale.Id,
                            ProductId = c.ProductId,
                            ProductName = c.Product.Name,
                            Cause = c.Cause,
                            DefectedQuantity = c.DefectedQuantity,
                            Quantity = c.Quantity,
                            ReturnDate = c.ReturnDate,
                            ReturnedPrice = c.ReturnedPrice,
                            ReturnedMoney = c.ReturnedMoney
                        };
            return query.ToList();
        }

        public ReturnedSaleDTO GetReturnedSale(int id)
        {
            var query = from c in db.ReturnedSales
                        where c.IsDeleted == false && c.Id == id
                        select new ReturnedSaleDTO()
                        {
                            Id = c.Id,
                            SaleId = c.SaleId,
                            ProductId = c.ProductId,
                            ProductName = c.Product.Name,
                            Cause = c.Cause,
                            DefectedQuantity = c.DefectedQuantity,
                            Quantity = c.Quantity,
                            ReturnDate = c.ReturnDate,
                            ReturnedPrice = c.ReturnedPrice,
                            ReturnedMoney = c.ReturnedMoney
                        };
            return query.FirstOrDefault();
        }

        public void Add(ReturnedSaleDTO c)
        {
            var element = new ReturnedSale()
            {
                Id = c.Id,
                SaleId = c.SaleId,
                ProductId = c.ProductId,
                Cause = c.Cause,
                DefectedQuantity = c.DefectedQuantity,
                Quantity = c.Quantity,
                ReturnDate = c.ReturnDate,
                ReturnedPrice = c.ReturnedPrice,
                ReturnedMoney = c.ReturnedMoney
            };
            db.ReturnedSales.InsertOnSubmit(element);
            db.SubmitChanges();
        }

        public void Update(ReturnedSaleDTO dto)
        {
            var element = (from c in db.ReturnedSales where c.Id == dto.Id select c).FirstOrDefault();
            element.Cause = dto.Cause;
            element.DefectedQuantity = dto.DefectedQuantity;
            element.Quantity = dto.Quantity;
            element.ReturnDate = dto.ReturnDate;
            element.ReturnedPrice = dto.ReturnedPrice;
            element.ReturnedMoney = dto.ReturnedMoney;
            element.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var element = (from c in db.ReturnedSales where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
