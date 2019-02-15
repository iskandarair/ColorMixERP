using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Server.BL
{
    public class ProductStockBL
    {
        public List<ProductStockDTO> GetProductStocks()
        {
            return new ProductStockDalFacade().GetProductStocks();
        }

        public ProductStockDTO GetProductStock(int? id)
        {
            return new ProductStockDalFacade().GetProductStock(id);
        }

        public void Add(int workPlaceId, ProductStockDTO stock)
        {
            new ProductStockDalFacade().Add(workPlaceId,stock);
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
    }
}
