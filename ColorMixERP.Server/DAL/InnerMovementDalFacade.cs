using System;
using System;
using System.Collections.Generic;
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
    public class InnerMovementDalFacade
    {
        private LinqContext db = null;
        public InnerMovementDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }

        public List<InnerMovement> GetInnerMovements()
        {
            var q = from c in db.InnerMovements where c.IsDeleted == false select c;
            return q.ToList();
        }

        public List<InnerMovementGroupDTO> GetInnerMovementDtos(InnerMovementCommand cmd,int workPlaceId, ref int pagesCount)
        {
            var query = from c in db.InnerMovements
                where c.FromWorkPlace.Id == workPlaceId
                group c by new { c.GroupId, c.CreatedDate, c.MoveDate.Date, c.FromWorkPlace, c.ToWorkPlace} into grp
                let productCount = grp.Select(x => x.Product.Id).Count()
                let totalPrice = grp.Sum(x => x.TotalPrice)
                select new InnerMovementGroupDTO()
                {
                    MoveDate = grp.Key.Date,
                    CreatedDate = grp.Key.CreatedDate,
                    FromWorkPlaceId = grp.Key.FromWorkPlace.Id.Value,
                    FromWorkPlaceName = grp.Key.FromWorkPlace.Name,
                    ToWorkPlaceId = grp.Key.ToWorkPlace.Id.Value,
                    ToWorkPlaceName = grp.Key.ToWorkPlace.Name,
                    ProductCount = productCount,
                    TotalPrice = totalPrice,
                    GroupId = grp.Key.GroupId
                };
            if (cmd.FilterWorkPlaceId > 0)
            {
                query = from p in query where p.FromWorkPlaceId == cmd.FilterWorkPlaceId select p;
            }

            if (cmd.FromPlaceId > 0 && cmd.ToPlaceId > 0)
            {
                query = from p in query where p.FromWorkPlaceId == cmd.FromPlaceId && p.ToWorkPlaceId == cmd.ToPlaceId select p;
            }

            if (cmd.MoveDate != null)
            {
                query = from p in query where p.MoveDate.Date >= cmd.MoveDate.Value.Date &&
                                              p.MoveDate.Date <= cmd.MoveDate.Value.Date
                        select p;
            }

            if (cmd.FromDate != null && cmd.ToDate != null)
            {
                query = from p in query
                    where p.MoveDate >= cmd.FromDate.Value.Date &&
                          p.MoveDate <= cmd.ToDate.Value.Date
                        select p;
            }
            

            if (cmd.SortByDate != null)
            {
                query = cmd.SortByDate == true
                    ? (from p in query orderby p.MoveDate select p)
                    : (from p in query orderby p.MoveDate descending select p);
            }

            if (cmd.SortByFromPlace != null)
            {
                query = cmd.SortByFromPlace == true
                    ? (from p in query orderby p.FromWorkPlaceName select p)
                    : (from p in query orderby p.FromWorkPlaceName descending select p);
            }

            if (cmd.SortByToPlace != null)
            {
                query = cmd.SortByToPlace == true
                    ? (from p in query orderby p.ToWorkPlaceName select p)
                    : (from p in query orderby p.ToWorkPlaceName descending select p);
            }

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count()/cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public InnerMovement GetInnerMovement(int id)
        {
            var q = from c in db.InnerMovements where c.Id == id select c;
            return q.FirstOrDefault();
        }

        public InnerMovementDTO GetInnerMovementDto(int id)
        {
            var q = from c in db.InnerMovements where c.Id == id select new InnerMovementDTO()
            {
                Id = c.Id,
                MoveDate = c.MoveDate,
                ProductId = c.Product.Id,
                ProductName = c.Product.Name,
                Quantity = c.Quantity,
                FromWorkPlaceId = c.FromWorkPlace.Id ?? 0,
                FromWorkPlaceName = c.FromWorkPlace.Name,
                ToWorkPlaceId = c.ToWorkPlace.Id ?? 0,
                ToWorkPlaceName = c.ToWorkPlace.Name,
                ProductPrice = c.Product.Price,
                TotalPrice = c.TotalPrice,
                CreatedDate = c.CreatedDate,
                GroupId = c.GroupId,
                CurrencyCode = c.Product.Currency
            };
            return q.FirstOrDefault();
        }
        public List< InnerMovementDTO> GetInnerMovementDtosStats(InnerMovementCommand cmd, ref int pagesCount)
        {
            var query = from c in db.InnerMovements
                where  c.IsDeleted == false
                select new InnerMovementDTO()
                {
                    Id = c.Id,
                    MoveDate = c.MoveDate,
                    ProductId = c.Product.Id,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity,
                    FromWorkPlaceId = c.FromWorkPlace.Id ?? 0,
                    FromWorkPlaceName = c.FromWorkPlace.Name,
                    ToWorkPlaceId = c.ToWorkPlace.Id ?? 0,
                    ToWorkPlaceName = c.ToWorkPlace.Name,
                    ProductPrice = c.Product.Price,
                    TotalPrice = c.TotalPrice,
                    CreatedDate = c.CreatedDate,
                    GroupId = c.GroupId,
                    CurrencyCode = c.Product.Currency
                };
            
            if (cmd.MoveDate != null)
            {
                query = from p in query
                    where p.MoveDate.Date >= cmd.MoveDate.Value.Date &&
                          p.MoveDate.Date <= cmd.MoveDate.Value.Date
                        select p;
            }

            if (cmd.FromDate != null && cmd.ToDate != null)
            {
                query = from p in query
                    where p.MoveDate >= cmd.FromDate.Value.Date &&
                          p.MoveDate <= cmd.ToDate.Value.Date
                        select p;
            }
            
            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count() / cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }
        public List<InnerMovementDTO> GetInnerMovementDtoByGroup(int groupId, DateTime craetedDate)
        {
            var q = from c in db.InnerMovements
                where c.GroupId == groupId && c.CreatedDate.Value >= craetedDate.AddDays(-1) && c.CreatedDate.Value <= craetedDate.AddDays(1)
                    select new InnerMovementDTO()
                {
                    Id = c.Id,
                    MoveDate = c.MoveDate,
                    ProductId = c.Product.Id,
                    ProductName = c.Product.Name,
                    Quantity = c.Quantity,
                    FromWorkPlaceId = c.FromWorkPlace.Id ?? 0,
                    FromWorkPlaceName = c.FromWorkPlace.Name,
                    ToWorkPlaceId = c.ToWorkPlace.Id ?? 0,
                    ToWorkPlaceName = c.ToWorkPlace.Name,
                    ProductPrice = c.Product.Price,
                    TotalPrice = c.TotalPrice,
                    CreatedDate = c.CreatedDate,
                    GroupId = c.GroupId,
                    CurrencyCode = c.Product.Currency
                };
            return q.ToList();
        }
        public void Add(InnerMovementDTO dto)
        {
            var elementToAdd = new InnerMovement(dto.Id, dto.MoveDate, dto.ProductId, dto.Quantity, dto.FromWorkPlaceId,
                    dto.ToWorkPlaceId)
            {
                CreatedDate = DateTime.Now.Date,
                GroupId = GetRandomGroupId(),
                TotalPrice = dto.TotalPrice,
            };
            db.InnerMovements.InsertOnSubmit(elementToAdd);
            db.SubmitChanges();
        }
        public void Add(List<InnerMovementDTO> dtos)
        {
            var groupId = GetRandomGroupId();
            var result = new  List<InnerMovement>();
            foreach (var dto in dtos)
            {
                result.Add(new InnerMovement(dto.Id, dto.MoveDate, dto.ProductId, dto.Quantity, dto.FromWorkPlaceId,
                    dto.ToWorkPlaceId)
                {
                    CreatedDate = DateTime.Now.Date,
                    GroupId = groupId,
                    TotalPrice = dto.TotalPrice,
                });
            }
            db.InnerMovements.InsertAllOnSubmit(result);
            db.SubmitChanges();
        }
        public void Update(InnerMovementDTO dto)
        {
            var elementToUpdate = GetInnerMovement(dto.Id ?? 0);
            elementToUpdate.Quantity = dto.Quantity;
            elementToUpdate.TotalPrice = dto.TotalPrice;
            elementToUpdate.MoveDate = dto.MoveDate;
            elementToUpdate.UpdatedDate = DateTime.Now;
            db.SubmitChanges();
        }
        public void Delete(int id)
        {
            var element = (from c in db.InnerMovements where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }

        public int GetRandomGroupId()
        {
            var query = (from c in db.InnerMovements  select c.GroupId).Max();
            if (query == null)
            {
                return 1; // Initial when database is empty
            }
            return (query.Value +1);
        }
    }
}
