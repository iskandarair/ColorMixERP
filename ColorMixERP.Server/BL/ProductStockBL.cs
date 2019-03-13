using ColorMixERP.Server.Entities;
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
    public class ProductStockBL
    {
        public List<ProductStockDTO> GetProductStocks(ProductStockCommand cmd, ref int pagesCount)
        {
            return new ProductStockDalFacade().GetProductStocks(cmd, ref pagesCount);
        }

        public List<ProductStockDTO> GetProductStockDtosByWp(int wpId, ProductStockCommand cmd, ref int pagesCount)
        {
            return new ProductStockDalFacade().GetProductStocksByWp(wpId, cmd, ref pagesCount);
        }

        public ProductStockDTO GetProductStock(int? id)
        {
            return new ProductStockDalFacade().GetProductStock(id);
        }

        public void Add(int workPlaceId, ProductStockDTO stock)
        {
            new ProductStockDalFacade().Add(workPlaceId, stock);
        }

        public void Update(ProductStockDTO stock)
        {
            new ProductStockDalFacade().Update(stock);
        }

        public void Delete(int? id)
        {
            new ProductStockDalFacade().Delete(id);
        }

        public List<ProductStockDTO> GetWorkPlaceProductStocks(int wpId)
        {
            return new ProductStockDalFacade().GetWorkPlaceProductStocks(wpId);
        }


        // To Keep up-to-Date with Novements and sales 
        public void UpdateProductStock(InnerMovementDTO dto)
        {
            var productInStockMinus =
                new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.FromWorkPlaceId, dto.ProductId);
            if (dto.Quantity > productInStockMinus.Quantity)
            {
                throw new ArgumentOutOfRangeException(
                    $"Not Enough Product ({productInStockMinus.ProductId}-{productInStockMinus.ProductName}) in ProductStock to complete transaction");
            }

            productInStockMinus.Quantity -= dto.Quantity;
            new ProductStockDalFacade().Update(productInStockMinus);

            var productInStockAdd =
                new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.ToWorkPlaceId, dto.ProductId);
            productInStockAdd.Quantity += dto.Quantity;
            new ProductStockDalFacade().Update(productInStockAdd);
        }

        public void UpdateProductStocks(List<InnerMovementDTO> dtos)
        {
            var errorMessage = string.Empty;
            foreach (var dto in dtos)
            {
                var productInStockMinus =
                    new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.FromWorkPlaceId, dto.ProductId);
                if (dto.Quantity > productInStockMinus.Quantity)
                {
                    errorMessage +=
                        $"Not Enough Product ({productInStockMinus.ProductId}-{productInStockMinus.ProductName}) in ProductStock to complete transaction.";

                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage += Environment.NewLine;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentOutOfRangeException(errorMessage);
            }

            foreach (var dto in dtos)
            {
                var productInStockMinus =
                    new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.FromWorkPlaceId, dto.ProductId);
                productInStockMinus.Quantity -= dto.Quantity;
                new ProductStockDalFacade().Update(productInStockMinus);

                var productInStockAdd =
                    new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.ToWorkPlaceId, dto.ProductId);
                productInStockAdd.Quantity += dto.Quantity;
                new ProductStockDalFacade().Update(productInStockAdd);

            }
        }

        public void UpdateProductStocks(OrderDTO order)
        {
            var errorMessage = string.Empty;
            var workplaceId = new UserDalFacade().GetAccountUser(order.SalerId).WorkPlaceId;
            foreach (var sale in order.Sales)
            {
                var productStock =
                    new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workplaceId, sale.ProductId);
                if (sale.Quantity > productStock.Quantity)
                {
                    errorMessage +=
                        $"Not Enough Product ({productStock.ProductId}-{productStock.ProductName}) in ProductStock to complete transaction.";
                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    errorMessage += Environment.NewLine;
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentOutOfRangeException(errorMessage);
            }
            foreach (var sale in order.Sales)
            {
                var productInStockMinus = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workplaceId, sale.ProductId);
                productInStockMinus.Quantity -= sale.Quantity;
                new ProductStockDalFacade().Update(productInStockMinus);
            }
        }

        public void UpdateProductStocks(SaleDTO sale, int userId)
        {
            var errorMessage = string.Empty;
            var workplaceId = new UserDalFacade().GetAccountUser(userId).WorkPlaceId;
            var productStock =
                new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workplaceId, sale.ProductId);
            if (sale.Quantity > productStock.Quantity)
            {
                errorMessage +=
                    $"Not Enough Product ({productStock.ProductId}-{productStock.ProductName}) in ProductStock to complete transaction.";
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                errorMessage += Environment.NewLine;
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                throw new ArgumentOutOfRangeException(errorMessage);
            }

            var productInStockMinus =
                new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workplaceId, sale.ProductId);
            productInStockMinus.Quantity -= sale.Quantity;
            new ProductStockDalFacade().Update(productInStockMinus);
        }

    }
}