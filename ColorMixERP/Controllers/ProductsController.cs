using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;

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
        public HttpResponseMessage AddProduct(Product product)
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
        public HttpResponseMessage UpdateProduct(Product product)
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
