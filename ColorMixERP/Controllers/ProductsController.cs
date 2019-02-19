using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Controllers
{
    public class ProductsController : ApiController
    {

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetProduct()
        {
            var data = new ProductBL().GetProducts();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetProductById(int id)
        {
            var data = new ProductBL().GetProduct(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddProduct(ProductDTO product)
        {
            try
            {
                new ProductBL().Add(product);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, true);
            }
        }


        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateProduct(ProductDTO product)
        {
            try
            {
                new ProductBL().Update(product);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }


        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                new ProductBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
