using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

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

        public List<InnerMovementDTO> GetInnerMovementDtos()
        {
            var q = from c in db.InnerMovements
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
            return q.ToList();
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

        public void Update(InnerMovementDTO dto)
        {
            var elementToUpdate = GetInnerMovement(dto.Id ?? 0);
           // elementToUpdate.Product = new ProductDalFacade().GetProduct(dto.ProductId);
            elementToUpdate.Quantity = dto.Quantity;
            elementToUpdate.MoveDate = dto.MoveDate;
            db.SubmitChanges();
        }
        public void Delete(int id)
        {
            var element = (from c in db.InnerMovements where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            db.SubmitChanges();
        }

    }
}
