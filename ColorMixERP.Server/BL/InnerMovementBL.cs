﻿using System;
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
        public List<InnerMovementDTO> GetInnerMovementDtos(InnerMovementCommand cmd, ref int pagesCount)
        {
            return new InnerMovementDalFacade().GetInnerMovementDtos(cmd, ref  pagesCount);
        }
        public InnerMovement GetInnerMovement(int id)
        {
            return  new InnerMovementDalFacade().GetInnerMovement(id);
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
                UserId = userId
            };
            new IncomeDalFacade().AddIncome(incomeDTO);
        }
        
        public void Add(List<InnerMovementDTO> dtos, int userId)
        {
            new ProductStockBL().UpdateProductStocks(dtos);
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
            };
            new IncomeDalFacade().AddIncome(incomeDTO);
            #endregion
        }

        public void Update(InnerMovementDTO dto)
        {
            new InnerMovementDalFacade().Update(dto);
        }
        public void Delete(int id)
        {
            new InnerMovementDalFacade().Delete(id);
        }
    }
}
