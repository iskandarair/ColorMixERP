using System;
using System.Collections.Generic;
using System.Data.Linq;
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
    public class IncomeDalFacade
    {
        private LinqContext db = null;
        public IncomeDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<ProductArrivalDTO> GetProductArrivals(IncomeCommand command, ref int pagesCount)
        {
            var query = from c in db.IncomeProducts
                group c by new {c.Product, c.CreatedDate.Value.Date } into grp
                let  SumQuantity = grp.Sum(x => x.Quantity)
                orderby grp.Key.Date
                select new ProductArrivalDTO()
                {
                    ProductCode = grp.Key.Product.Code,
                    ProductName = grp.Key.Product.Name,
                    Date = grp.Key.Date,
                    Quantity = SumQuantity,
                    SupplierName = grp.Key.Product.Supplier.Name
                };

            if (command.Date != null)
            {
                query = from p in query where p.Date.ToString("d") == command.Date.Value.ToString("d") select p;
            }

            if (command.FromDate != null && command.ToDate != null)
            {
                query = from p in query
                    where p.Date >= command.FromDate.Value &&
                          p.Date <= command.ToDate.Value
                    select p;
            }
            
            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / command.PageSize);
            query = query.Page(command.PageSize, command.Page);
            return query.ToList();
        }
        public List<IncomeDTO> GetIncomes(IncomeCommand command, ref int pagesCount)
        {
            var query = from c in db.Incomes
                select new IncomeDTO()
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    UserName = c.User.Name + " " + c.User.Surname,
                    FromWorkplaceId = c.FromWorkPlaceId,
                    FromWorkplaceName = c.FromWorkPlace.Name,
                    ToWorkplaceId = c.ToWorkplaceId,
                    ToWorkplaceName = c.ToWorkPlace.Name,
                    CreatedDate = c.CreatedDate.Value,
                    UpdatedDate = c.UpdatedDate.Value,
                    IsProductStock = c.IsProductStock,
                    IncomeProducts = GetIncomeProducts(c.Id),
                };

            if (command.UserId > 0)
            {
                query = from c in query where c.UserId == command.UserId select c;
            }

            if (command.FromWorkplace > 0)
            {
                query = from c in query where c.FromWorkplaceId == command.FromWorkplace select c;
            }

            if (command.ToWorkplace > 0)
            {
                query = from c in query where c.ToWorkplaceId == command.ToWorkplace select c;
            }

            if (command.Date != null)
            {
                query = from p in query where p.CreatedDate.ToString("d") == command.Date.Value.ToString("d") select p;
            }

            if (command.FromDate != null && command.ToDate != null)
            {
                query = from p in query
                    where p.CreatedDate >= command.FromDate.Value &&
                          p.CreatedDate <= command.ToDate.Value
                    select p;
            }

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / command.PageSize);
            query = query.Page(command.PageSize, command.Page);

            return query.ToList();
        }


        public IncomeDTO GetIncome(int id)
        {
            var query = from c in db.Incomes
                where c.Id == id
                select new IncomeDTO()
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    UserName = c.User.Name + " " + c.User.Surname,
                    FromWorkplaceId = c.FromWorkPlaceId,
                    FromWorkplaceName = c.FromWorkPlace.Name,
                    ToWorkplaceId = c.ToWorkplaceId,
                    ToWorkplaceName = c.ToWorkPlace.Name,
                    CreatedDate = c.CreatedDate.Value,
                    UpdatedDate = c.UpdatedDate.Value,
                    IsProductStock = c.IsProductStock
                };
             
            return query.FirstOrDefault();
        }
        public List<IncomeDTO> GetIncomes(int[] ids)
        {
            var query = from c in db.Incomes
                where ids.Contains(c.Id)
                select new IncomeDTO()
                {
                    Id = c.Id,
                    UserId = c.UserId,
                    UserName = c.User.Name + " " + c.User.Surname,
                    FromWorkplaceId = c.FromWorkPlaceId,
                    FromWorkplaceName = c.FromWorkPlace.Name,
                    ToWorkplaceId = c.ToWorkplaceId,
                    ToWorkplaceName = c.ToWorkPlace.Name,
                    CreatedDate = c.CreatedDate.Value,
                    UpdatedDate = c.UpdatedDate.Value,
                    IsProductStock = c.IsProductStock
                };

            return query.ToList();
        }
        public List<IncomeProductDTO> GetIncomeProducts (int incomeId)
        {
            var query = from c in db.IncomeProducts where c.IncomeId == incomeId
                select new IncomeProductDTO()
                {
                    Id = c.Id,
                    IncomeId = c.IncomeId,
                    ProductId =  c.ProductId,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity,
                    CreatedDate = c.CreatedDate.Value,
                    UpdatedDate = c.UpdatedDate.Value,
                };

            return query.ToList();
        }
        public IncomeProductDTO GetIncomeProductById(int id)
        {
            var query = from c in db.IncomeProducts
                where c.Id == id
                        select new IncomeProductDTO()
                {
                    Id = c.Id,
                    IncomeId = c.IncomeId,
                    ProductId = c.ProductId,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity,
                    CreatedDate = c.CreatedDate.Value,
                    UpdatedDate = c.UpdatedDate.Value,
                };

            return query.FirstOrDefault();
        }

        public void AddIncome(IncomeDTO dto)
        {
            var income = new Income()
            {
                UserId = dto.UserId,
                FromWorkPlaceId = dto.FromWorkplaceId,
                ToWorkplaceId =  dto.FromWorkplaceId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsProductStock = dto.IsProductStock,
                IncomeProducts = new EntitySet<IncomeProduct>()
            };
            db.Incomes.InsertOnSubmit(income);
            db.SubmitChanges();
            AddIncomeProducts(income.Id, dto.IncomeProducts);
        }

        public void AddIncomeProducts(int incomeId,List<IncomeProductDTO> dtos)
        {
            var result = new List<IncomeProduct>();
            foreach (var c in dtos)
            {
                result.Add(new IncomeProduct()
                {
                    IncomeId = incomeId,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                });
            }
            db.IncomeProducts.InsertAllOnSubmit(result);
            db.SubmitChanges();
        }

        public void Update(IncomeProductDTO dto)
        {
            var elementToUpdate = (from c in db.IncomeProducts where c.Id == dto.Id select c).FirstOrDefault();
            elementToUpdate.Quantity = dto.Quantity;
            elementToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
