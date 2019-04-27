using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class DailyBalanceDalFacade
    {
        private LinqContext db = null;
        public DailyBalanceDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<DailyBalanceDTO> GetDailyBalances()
        {
            var query = from c in db.DailyBalances
                select new DailyBalanceDTO()
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    Product = ProductMapper(c.Product),// new ProductDalFacade().GetProduct(c.Id),
                    BalanceDate = c.BalanceDate,
                    Quantity = c.Quantity,
                    WorkPlaceId = c.WorkPlaceId,
                    WorkPlace = new WorkPlaceDalFacade().GetWorkPlace(c.WorkPlaceId),
                };
            return query.ToList();
        }

        public List<DailyBalanceDTO> GetDailyBalancesStats(DailyBalanceCommand cmd,int workplaceId, bool isAdmin, ref int pagesCount)
        {
            IQueryable<DailyBalanceDTO> query;

            if (isAdmin)
            {
                if (cmd.TargetWorkPlace.Value > 0)
                {
                    query = from c in db.DailyBalances
                        where c.WorkPlaceId == cmd.TargetWorkPlace
                        group c by new { c.Product, c.BalanceDate.Date }
                        into grp
                        let sumQuantity = grp.Sum(x => x.Quantity)
                        select new DailyBalanceDTO()
                        {
                            ProductId = grp.Key.Product.Id,
                            Product = ProductMapper(grp.Key.Product),
                            BalanceDate = grp.Key.Date,
                            Quantity = sumQuantity
                        };
                }
                else
                {
                    query = from c in db.DailyBalances
                        group c by new {c.Product, c.BalanceDate.Date}
                        into grp
                        let sumQuantity = grp.Sum(x => x.Quantity)
                        select new DailyBalanceDTO()
                        {
                            ProductId = grp.Key.Product.Id,
                            Product = ProductMapper(grp.Key.Product),
                            BalanceDate = grp.Key.Date,
                            Quantity = sumQuantity
                        };
                }
            }
            else
            {
                query = from c in db.DailyBalances
                    where c.WorkPlaceId == workplaceId
                    group c by new { c.Product, c.BalanceDate.Date }
                    into grp
                    let sumQuantity = grp.Sum(x => x.Quantity)
                    select new DailyBalanceDTO()
                    {
                        ProductId = grp.Key.Product.Id,
                        Product = ProductMapper(grp.Key.Product),
                        BalanceDate = grp.Key.Date,
                        Quantity = sumQuantity
                    };
            }

            if (cmd.Date != null)
            {
                query = from c in query
                    where c.BalanceDate >= cmd.Date.Value.Date
                          && c.BalanceDate <= cmd.Date.Value.Date
                        select c;
            }

            if (cmd.FromDate != null && cmd.ToDate != null)
            {
                query = from p in query
                    where p.BalanceDate.Date == cmd.FromDate.Value.Date ||
                          p.BalanceDate.Date == cmd.ToDate.Value.Date
                        select p;
            }
            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public DailyBalanceDTO GetDailyBalance(int id)
        {
            var query = from c in db.DailyBalances
                where c.Id == id
                select new DailyBalanceDTO()
                {
                    Id = c.Id,
                    ProductId = c.ProductId,
                    Product = ProductMapper(c.Product),
                    BalanceDate = c.BalanceDate,
                    Quantity = c.Quantity,
                    WorkPlaceId = c.WorkPlaceId,
                    WorkPlace = new WorkPlaceDalFacade().GetWorkPlace(c.WorkPlaceId),
                };
            return query.FirstOrDefault();
        }

        public void Add(DailyBalanceDTO dto)
        {
            var dailyBalance = new DailyBalance()
            {
                BalanceDate = dto.BalanceDate.Date,
                ProductId = dto.Product.Id.Value,
                Quantity = dto.Quantity,
                WorkPlaceId = dto.WorkPlaceId,
            };
            db.DailyBalances.InsertOnSubmit(dailyBalance);
            db.SubmitChanges();
        }

        public void Add(List<DailyBalanceDTO> dtos)
        {
            var result = new List<DailyBalance>();

            foreach (var dto in dtos)
            {
                var dailyBalance = new DailyBalance()
                {
                    BalanceDate = dto.BalanceDate.Date,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    WorkPlaceId = dto.WorkPlaceId,
                };
                result.Add(dailyBalance);
            }
            db.DailyBalances.InsertAllOnSubmit(result);
            db.SubmitChanges();
        }
        public void Update(DailyBalanceDTO dto)
        {
            var balance = (from c in db.DailyBalances where c.Id == dto.Id select c).FirstOrDefault();
            balance.Quantity = dto.Quantity;
            db.SubmitChanges();
        }

        public void Delete(int id)
        {
            var balance = (from c in db.DailyBalances where c.Id == id select c).FirstOrDefault();
            db.DailyBalances.DeleteOnSubmit(balance);
            db.SubmitChanges();
        }


        private static ProductDTO ProductMapper(Product product)
        {
            return new ProductDTO()
            {
                Id = product.Id,
                Code = product.Code,
                Category = new CategoryDTO()
                {
                    Id = product.Category.Id.Value,
                    Code = product.Category.Code,
                    Name = product.Category.Name
                },
                BoxedNumber = product.BoxedNumber,
                Currency = product.Currency,
                MeasurementUnit = product.MeasurementUnit,
                Price = product.Price,
                Name = product.Name,
                Supplier = new SupplierDTO()
                {
                    Id = product.SupplierId,
                    Name = product.Supplier.Name,
                    SupplierInfo = product.Supplier.SupplierInfo
                }
            };
        }

    }
}
