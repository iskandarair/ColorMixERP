﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Server.BL
{
    public class SaleBL
    {
        public List<SaleDTO> GetSales(SaleCommand cmd, ref int pagesCount)
        {
            return new SaleDalFacade().GetSales(cmd, ref pagesCount);
        }

        public List<SaleDTO> GetOrderSale(int orderId)
        {
            return new SaleDalFacade().GetOrderSale(orderId);
        }

        public SaleDTO GetSale(int id)
        {
            return new SaleDalFacade().GetSale(id);
        }

        public decimal GetLatestCurrencyRate()
        {
            return new SaleDalFacade().GetLatestCurrencyRate();
        }
        public void Add(SaleDTO sale)
        {
            new SaleDalFacade().Add(sale);
        }

        public void AddRange(List<SaleDTO> sales,int orderId)
        {
            new SaleDalFacade().AddRange(sales, orderId);
        }

        public void Update(SaleDTO sale, int userId)
        {
            var saleOld = new SaleDalFacade().GetSale(sale.Id);
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id.Value;
            var diff = sale.Quantity - saleOld.Quantity; // should-Be - was
            var productInStockFrom = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workPlaceId, sale.ProductId);
            if (productInStockFrom.Quantity > diff)
            {
                productInStockFrom.Quantity -= diff;// dto.Quantity - inMovement.Quantity;
                new ProductStockDalFacade().Update(productInStockFrom);
            }
            else if (diff == 0)
            {
                //do nothing
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Not Enough Product ({saleOld.ProductId}-{saleOld.ProductName}) in ProductStock to complete transaction.");
            }
            new SaleDalFacade().Update(saleOld);
        }

        public void Delete(int id)
        {
            var sale = new SaleDalFacade().GetSale(id);
            var order = new OrderBL().GetClientOrder(sale.OrderId);
            var saler = new UserBL().GetAccountUser(order.SalerId);
            if (saler.WorkPlace.Id != null)
            {
                var productStock = new ProductStockDTO()
                {
                    WorkPlaceId = saler.WorkPlace.Id.Value,
                    ProductId = sale.ProductId,
                    Quantity = sale.Quantity
                };
                new ProductStockBL().Add(productStock, order.SalerId);
            }

            new SaleDalFacade().Delete(id);
        }
    }
}
