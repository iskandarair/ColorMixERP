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

        public List<InnerMovementDTO> GetInnerMovementDtos(InnerMovementCommand cmd, ref int pagesCount)
        {
            var query = from c in db.InnerMovements
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
                    ToWorkPlaceName =  c.ToWorkPlace.Name,
                };

            if (cmd.ProductId > 0)
            {
                query = from p in query where p.ProductId == cmd.ProductId select p;
            }
            if (cmd.FromPlaceId > 0)
            {
                query = from p in query where p.FromWorkPlaceId ==cmd.FromPlaceId select p;
            }
            if (cmd.ToPlaceId > 0)
            {
                query = from p in query where p.ToWorkPlaceId == cmd.ToPlaceId select p;
            }

            if (cmd.MoveDate != null)
            {
                query = from p in query where p.MoveDate.ToString("d") == cmd.MoveDate.Value.ToString("d") select p;
            }

            if (cmd.FromDate != null && cmd.ToDate != null)
            {
                query = from p in query
                    where p.MoveDate >= cmd.FromDate.Value &&
                          p.MoveDate <= cmd.ToDate.Value
                    select p;
            }

            if (cmd.SortByProduct != null)
            {
                query = cmd.SortByProduct == true
                    ? (from p in query orderby p.ProductName select p)
                    : (from p in query orderby p.ProductName descending select p);
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
                ToWorkPlaceName = c.ToWorkPlace.Name
            };
            return q.FirstOrDefault();
        }

        public void Add(InnerMovementDTO dto)
        {
            var elementToAdd = new InnerMovement(dto.Id, dto.MoveDate, dto.ProductId, dto.Quantity, dto.FromWorkPlaceId, dto.ToWorkPlaceId);
            db.InnerMovements.InsertOnSubmit(elementToAdd);
            db.SubmitChanges();
        }
        public void Add(List<InnerMovementDTO> dtos)
        {
            var result = new  List<InnerMovement>();
            foreach (var dto in dtos)
            {
                result.Add(new InnerMovement(dto.Id, dto.MoveDate, dto.ProductId, dto.Quantity, dto.FromWorkPlaceId, dto.ToWorkPlaceId));
            }
            db.InnerMovements.InsertAllOnSubmit(result);
            db.SubmitChanges();
        }
        public void Update(InnerMovementDTO dto)
        {
            var elementToUpdate = GetInnerMovement(dto.Id ?? 0);
           // elementToUpdate.Product = new ProductDalFacade().GetProduct(dto.ProductId);
            elementToUpdate.Quantity = dto.Quantity;
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

    }
}
