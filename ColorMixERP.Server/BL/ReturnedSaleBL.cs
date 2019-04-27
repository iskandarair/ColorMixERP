using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class ReturnedSaleBL
    {
        public List<ReturnedSaleDTO> GetReturnedSales(PaginationDTO cmd, int userId, ref int pagesCount)
        {
            
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id.Value;
            var isAdmin = new UserBL().GetAccountUser(userId)?.PositionRole == 1;
            return new ReturnedSaleDalFacade().GetReturnedSales(cmd, workPlaceId, isAdmin, ref pagesCount);
        }

        public List<ReturnedSaleDTO> GetOrderReturnedSale(int orderId)
        {
            return new ReturnedSaleDalFacade().GetOrderReturnSale(orderId);
        }

        public ReturnedSaleDTO GetReturnedSale(int id)
        {
            return new ReturnedSaleDalFacade().GetReturnedSale(id);
        }

        public void Add(ReturnedSaleDTO dto, int userId)
        {
            if (dto.SaleId == 0)
            {
                dto.SaleId = null;
            }
            new ProductStockBL().UpdateProductStock(dto, userId);
            new ReturnedSaleDalFacade().Add(dto);

            var lostInMoney = dto.ReturnedPrice * dto.Quantity;
           //var sale = new SaleDalFacade().GetSale(dto.SaleId);
           //sale.Quantity -= dto.Quantity;
           //sale.SalesPrice -= lostInMoney;
           //new SaleDalFacade().Update(sale);

           //var order = new OrderDalFacade().GetClientOrder(sale.OrderId);
           //order.OverallPrice -= lostInMoney;
           //order.PaymentByCash -= lostInMoney;
           //new OrderDalFacade().Update(order);
        }


        public void Update(ReturnedSaleDTO dto, int userId)
        {
            //var sale = new SaleDalFacade().GetSale(dto.SaleId);
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id.Value;
            var existingReturnedSale = GetReturnedSale(dto.Id);
            var dasd = existingReturnedSale.Quantity - existingReturnedSale.DefectedQuantity;
            var diff = (dto.Quantity - dto.DefectedQuantity) - dasd; // quantity - deffectedQuantity !!!(So quantity is general for both)

            var productInStockFrom = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workPlaceId, dto.ProductId);
            productInStockFrom.Quantity += diff;

            new ProductStockDalFacade().Update(productInStockFrom);
            
            new ReturnedSaleDalFacade().Update(dto);
            //

            var existingReturnSale = new ReturnedSaleBL().GetReturnedSale(dto.Id);
            var lostInMoneyInInsert = existingReturnSale.ReturnedPrice * existingReturnSale.Quantity;
            var lostInMoneyInUpdate = dto.ReturnedPrice * dto.Quantity;
            var lostInMoney = lostInMoneyInInsert - lostInMoneyInUpdate;
            var quantity = existingReturnSale.Quantity - dto.Quantity;
            //var sale2 = new SaleDalFacade().GetSale(dto.SaleId);
            //sale2.SalesPrice -= lostInMoney;
            //sale2.Quantity -= quantity;
            //new SaleDalFacade().Update(sale2);

           //var order = new OrderDalFacade().GetClientOrder(sale.OrderId);
           //order.OverallPrice -= lostInMoney;
           //order.PaymentByCash -= lostInMoney;
           //new OrderDalFacade().Update(order);
        }

        public void Delete(int id, int userId)
        {
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id.Value;
            var existing = GetReturnedSale(id);
            var existingDiff = existing.Quantity - existing.DefectedQuantity;
            var productInStockFrom = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workPlaceId, existing.ProductId);
            productInStockFrom.Quantity -= existingDiff;

            new ProductStockDalFacade().Update(productInStockFrom);

            new ReturnedSaleDalFacade().Delete(id);
        }
    }
}
