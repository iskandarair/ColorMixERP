using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.BL;

namespace ColorMixERP.Controllers
{
    public class ReturnedSalesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var data = new ReturnedSaleBL().GetReturnedSales();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var data = new ReturnedSaleBL().GetReturnedSale(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(ReturnedSaleDTO element)
        {
            try
            {
                new ReturnedSaleBL().Add(element);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update(ReturnedSaleDTO dto)
        {
            try
            {
                new ReturnedSaleBL().Update(dto);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                new ReturnedSaleBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

    }
}
