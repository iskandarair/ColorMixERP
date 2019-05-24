using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class InnerMovementBL
    {
        public List<InnerMovement> GetInnerMovements()
        {
            return new InnerMovementDalFacade().GetInnerMovements();
        }

        public List<InnerMovementDTO> GetInnerMovementDtoByGroup(int groupId, DateTime createdDate)
        {
            return new InnerMovementDalFacade().GetInnerMovementDtoByGroup(groupId, createdDate);
        }
        public List<InnerMovementGroupDTO> GetInnerMovementDtos(InnerMovementCommand cmd,int userId, ref int pagesCount)
        {

            var userData = new UserBL().GetAccountUser(userId);
            var workPlaceId = userData.WorkPlace.Id.Value;
            return new InnerMovementDalFacade().GetInnerMovementDtos(cmd, workPlaceId, ref  pagesCount);
        }
        public InnerMovement GetInnerMovement(int id)
        {
            return  new InnerMovementDalFacade().GetInnerMovement(id);
        }

        public List<InnerMovementDTO> GetInnerMovementDtosStats(InnerMovementCommand cmd, ref int pagesCount)
        {
            return new InnerMovementDalFacade().GetInnerMovementDtosStats(cmd, ref pagesCount);
        }
        public InnerMovementDTO GetInnerMovementDto(int id)
        {
            return new InnerMovementDalFacade().GetInnerMovementDto(id);
        }

        public void Add(InnerMovementDTO dto, int userId)
        {
            new ProductStockBL().UpdateProductStock(dto);
            
            new InnerMovementDalFacade().Add(dto);
            var incomeProducts = new List<IncomeProductDTO>();
            incomeProducts.Add(new IncomeProductDTO()
            {
                ProductId =  dto.ProductId,
                Quantity =  dto.Quantity
            });
            var incomeDTO = new IncomeDTO()
            {
                FromWorkplaceId = dto.FromWorkPlaceId,
                ToWorkplaceId = dto.ToWorkPlaceId,
                IncomeProducts = incomeProducts,
                IsProductStock = false,
                UserId = userId
            };
            new IncomeDalFacade().AddIncome(incomeDTO);
        }
        
        public void Add(List<InnerMovementDTO> dtos, int userId)
        {
            new ProductStockBL().UpdateProductStocks(dtos);
            new InnerMovementDalFacade().Add(dtos);
            #region Income
            // INCOME SECTION
            var incomeProducts = new List<IncomeProductDTO>();
            foreach (var dto in dtos)
            {
                incomeProducts.Add(new IncomeProductDTO()
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                });
            }
            var incomeDTO = new IncomeDTO()
            {
                FromWorkplaceId = dtos.FirstOrDefault().FromWorkPlaceId,
                ToWorkplaceId = dtos.FirstOrDefault().ToWorkPlaceId,
                UserId = userId,
                IncomeProducts = incomeProducts,
                IsProductStock = false,
            };
            new IncomeDalFacade().AddIncome(incomeDTO);
            #endregion
        }

        public void Update(InnerMovementDTO dto, int userId)
        {
            var inMovement = new InnerMovementDalFacade().GetInnerMovement(dto.Id.Value);
            var diff = dto.Quantity - inMovement.Quantity; // should-Be - was
            var productInStockFrom = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.FromWorkPlaceId, dto.ProductId);

            var productInStockTo = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.ToWorkPlaceId, dto.ProductId);
            if (diff != 0 && productInStockFrom.Quantity > diff && (productInStockTo.Quantity + diff) >=0)
            {
                productInStockFrom.Quantity -= diff;// dto.Quantity - inMovement.Quantity;
                new ProductStockDalFacade().Update(productInStockFrom);
                
                productInStockTo.Quantity += diff;
                new ProductStockDalFacade().Update(productInStockTo);
            }
            else if (diff == 0)
            {
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Not Enough Product ({dto.ProductId}-{dto.ProductName}) in ProductStock to complete transaction.");
            }

            var quantity = inMovement.Quantity - dto.Quantity;
            var incomeProducts = new List<IncomeProductDTO>();
            incomeProducts.Add(new IncomeProductDTO()
            {
                ProductId = dto.ProductId,
                Quantity = quantity
            });
            var incomeDTO = new IncomeDTO()
            {
                FromWorkplaceId = dto.FromWorkPlaceId,
                ToWorkplaceId = dto.ToWorkPlaceId,
                IncomeProducts = incomeProducts,
                IsProductStock = false,
                UserId = userId
            };
            new IncomeDalFacade().AddIncome(incomeDTO);
            new InnerMovementDalFacade().Update(dto);
        }

        public void Update(InnerMovementDTO[] dtos, int userId)
        {
            string exMsg = null;
            foreach (var dto in dtos)
            {
                var inMovement = new InnerMovementDalFacade().GetInnerMovement(dto.Id.Value);
                var diff = dto.Quantity - inMovement.Quantity; // should-Be - was
                var productInStockFrom = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.FromWorkPlaceId, dto.ProductId);

                var productInStockTo = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.ToWorkPlaceId, dto.ProductId);
                if (diff != 0 && productInStockFrom.Quantity > diff && (productInStockTo.Quantity + diff) >= 0 )
                {
                    productInStockFrom.Quantity -= diff;// dto.Quantity - inMovement.Quantity;
                    new ProductStockDalFacade().Update(productInStockFrom);
                    
                    productInStockTo.Quantity += diff;
                    new ProductStockDalFacade().Update(productInStockTo);
                }
                else if (diff == 0)
                {
                }
                else
                {
                    exMsg = $"Not Enough Product ({dto.ProductId}-{dto.ProductName}) in ProductStock to complete transaction.";
                }
            }

            if (exMsg != null)
            {
                throw new ArgumentOutOfRangeException(exMsg);
            }
            foreach (var dto in dtos)
            {
                var inMovement = new InnerMovementDalFacade().GetInnerMovement(dto.Id.Value);
                var quantity = inMovement.Quantity - dto.Quantity;
                var incomeProducts = new List<IncomeProductDTO>();
                incomeProducts.Add(new IncomeProductDTO()
                {
                    ProductId = dto.ProductId,
                    Quantity = quantity
                });
                var incomeDTO = new IncomeDTO()
                {
                    FromWorkplaceId = dto.FromWorkPlaceId,
                    ToWorkplaceId = dto.ToWorkPlaceId,
                    IncomeProducts = incomeProducts,
                    IsProductStock = false,
                    UserId = userId
                };
                new IncomeDalFacade().AddIncome(incomeDTO);
                new InnerMovementDalFacade().Update(dto);
            }
        }
        public void Delete(int id)
        {
            new InnerMovementDalFacade().Delete(id);
        }
    }
}
