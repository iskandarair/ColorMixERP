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

        public void Add(ProductStockDTO stock, int userId)
        {
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id;
            var incomeProducts = new List<IncomeProductDTO>();

            var income = CreateLogIncome(userId, stock.ProductId, stock.Quantity, workPlaceId);
            new IncomeDalFacade().AddIncome(income);
            new ProductStockDalFacade().Add(stock);
        }

        public void Update(ProductStockDTO stock, int userId)
        {
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id;
            var existedStock = new ProductStockDalFacade().GetProductStock(stock.Id);
            var quantity = stock.Quantity - existedStock.Quantity;
            var income = CreateLogIncome(userId, existedStock.ProductId, quantity, workPlaceId);
            new IncomeDalFacade().AddIncome(income);
            new ProductStockDalFacade().Update(stock);
        }

        public void Delete(int? id, int userId)
        {
            var workPlaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id;
            var existedStock = new ProductStockDalFacade().GetProductStock(id);
            var quantity = existedStock.Quantity * -1;
            var income = CreateLogIncome(userId, existedStock.ProductId, quantity, workPlaceId);
            new IncomeDalFacade().AddIncome(income);
            new ProductStockDalFacade().Delete(id);
        }

        private IncomeDTO CreateLogIncome(int userId, int productId, decimal quantity, int? workPlaceId)
        {
            var incomeProducts = new List<IncomeProductDTO>();
            incomeProducts.Add(new IncomeProductDTO()
            {
                ProductId = productId,
                Quantity = quantity
            });
            var income = new IncomeDTO()
            {
                FromWorkplaceId = workPlaceId.Value,
                ToWorkplaceId = workPlaceId.Value,
                UserId = userId,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsProductStock = true,
                IncomeProducts = incomeProducts
            };
            return income;
        }

        public List<ProductStockDTO> GetWorkPlaceProductStocks(int wpId)
        {
            return new ProductStockDalFacade().GetWorkPlaceProductStocks(wpId);
        }


        // To Keep up-to-Date with Movements and sales 
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

            var productInStockAdd = new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.ToWorkPlaceId, dto.ProductId);
            if (productInStockAdd == null)
            {
                AddProductStock(dto);
            }
            else
            {
                productInStockAdd.Quantity += dto.Quantity;
                new ProductStockDalFacade().Update(productInStockAdd);
            }
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
                    new ProductStockDalFacade().GetProductStockByPlaceAndProduct(dto.ToWorkPlaceId, dto.ProductId, true);
                if (productInStockAdd == null)
                {
                    AddProductStock(dto);
                }
                else
                {
                    productInStockAdd.Quantity += dto.Quantity;
                    new ProductStockDalFacade().Update(productInStockAdd);
                }

            }
        }

        public void UpdateProductStocks(OrderDTO order)
        {
            var errorMessage = string.Empty;
            var workplaceId = new UserDalFacade().GetAccountUser(order.SalerId).WorkPlace.Id.Value;
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
            var workplaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id.Value;
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

        public void UpdateProductStock(ReturnedSaleDTO dto, int userId)
        {
            var workplaceId = new UserDalFacade().GetAccountUser(userId).WorkPlace.Id.Value;
            var productId = new SaleDalFacade().GetSale(dto.SaleId).ProductId;

            var productInStockAdd =
                new ProductStockDalFacade().GetProductStockByPlaceAndProduct(workplaceId, productId);
            if (productInStockAdd == null)
            {
                AddProductStock(workplaceId,productId,dto.Quantity);
            }
            else
            {
                productInStockAdd.Quantity += dto.Quantity;
                new ProductStockDalFacade().Update(productInStockAdd);
            }
        }

        //
        private static void AddProductStock(InnerMovementDTO dto)
        {
            var productStock = new ProductStockDTO()
            {
                WorkPlaceId = dto.ToWorkPlaceId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
            };
            new ProductStockDalFacade().Add(productStock);
        }
        private static void AddProductStock(int workplaceId, int productId, decimal quantity)
        {
            var productStock = new ProductStockDTO()
            {
                WorkPlaceId = workplaceId,
                ProductId = productId,
                Quantity = quantity,
            };
            new ProductStockDalFacade().Add( productStock);
        }
    }
}