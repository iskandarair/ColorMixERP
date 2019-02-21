using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Server.DAL
{
    public class ReturnedSaleDalFacade
    {
        private LinqContext db;

        public ReturnedSaleDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ReturnedSaleDTO> GetReturnedSales()
        {
            var query = from c in db.ReturnedSales
                where c.IsDeleted == false
                select new ReturnedSaleDTO()
                {
                    Id = c.Id,
                    SaleId = c.Sale.Id,
                    Cause = c.Cause,
                    DefectedQuantity = c.DefectedQuantity,
                    Quantity = c.Quantity,
                    ReturnDate = c.ReturnDate,
                    ReturnedPrice = c.ReturnedPrice
                };
            return query.ToList();
        }

        public List<ReturnedSaleDTO> GetOrderReturnSale(int orderId)
        {
                var query = from c in db.ReturnedSales
                    where c.IsDeleted == false &&  c.Sale.OrderId == orderId
                    select new ReturnedSaleDTO()
                    {
                        Id = c.Id,
                        SaleId = c.Sale.Id,
                        Cause = c.Cause,
                        DefectedQuantity = c.DefectedQuantity,
                        Quantity = c.Quantity,
                        ReturnDate = c.ReturnDate,
                        ReturnedPrice = c.ReturnedPrice
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
                    SaleId = c.Sale.Id,
                    Cause = c.Cause,
                    DefectedQuantity = c.DefectedQuantity,
                    Quantity = c.Quantity,
                    ReturnDate = c.ReturnDate,
                    ReturnedPrice = c.ReturnedPrice
                };
            return query.FirstOrDefault();
        }


        public void Add(ReturnedSaleDTO c)
        {
            var element = new ReturnedSale()
            {
                Id = c.Id,
                SaleId = c.SaleId,
                Cause = c.Cause,
                DefectedQuantity = c.DefectedQuantity,
                Quantity = c.Quantity,
                ReturnDate = c.ReturnDate,
                ReturnedPrice = c.ReturnedPrice
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
