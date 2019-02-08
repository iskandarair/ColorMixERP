using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.DAL;

namespace ColorMixERP.Server.BL
{
    public class ProductStockBL
    {
        public List<ProductStock> GetProductStocks()
        {
            return new ProductStockDalFacade().GetProductStocks();
        }

        public ProductStock GetProductStock(int? id)
        {
            return new ProductStockDalFacade().GetProductStock(id);
        }

        public void Add(int workPlaceId, ProductStock stock)
        {
            new ProductStockDalFacade().Add(workPlaceId,stock);
        }
        
        public void Update( ProductStock stock)
        {
            new ProductStockDalFacade().Update(stock);
        }

        public void Delete(int? id)
        {
            new ProductStockDalFacade().Delete(id);
        }

        public List<ProductStock> GetWorkPlaceProductStocks(int wpId)
        {
           return new ProductStockDalFacade().GetWorkPlaceProductStocks(wpId);
        }
    }
}
