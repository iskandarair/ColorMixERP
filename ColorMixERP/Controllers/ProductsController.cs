using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Logging;
using ColorMixERP.Models;
using Newtonsoft.Json;

namespace ColorMixERP.Controllers
{
    public class ProductsController : ApiController
    {

        [Authorize]
        [HttpGet]
        [Route("api/Products/")]
        public HttpResponseMessage GetProduct(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<ProductCommand>(query);
                int pagesCount = 0;
                var data = new ProductBL().GetProducts(cmd, ref pagesCount);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                result.Headers.Add(Consts.PAGES_COUNT, pagesCount.ToString());
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/Products/{id}")]
        public HttpResponseMessage GetProductById(int id)
        {
            try
            {
                var data = new ProductBL().GetProduct(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/Products")]
        public HttpResponseMessage AddProduct(ProductDTO product)
        {
            try
            {
                new ProductBL().Add(product);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, true);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/Products/Range")]
        public HttpResponseMessage AddProduct(List<ProductDTO> productDto)
        {
            try
            {
                new ProductBL().AddRange(productDto);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        [Route("api/Products/")]
        public HttpResponseMessage UpdateProduct(ProductDTO product)
        {
            try
            {
                new ProductBL().Update(product);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("api/Products/")]
        public HttpResponseMessage DeleteProduct(int id)
        {
            try
            {
                new ProductBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
