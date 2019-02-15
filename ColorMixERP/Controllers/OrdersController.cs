using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Controllers
{
    public class OrdersController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetOrders()
        {
            var data  = new OrderBL().GetClientOrders();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetOrder(int id)
        {
            var data = new OrderBL().GetClientOrder(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddUser(OrderDTO order)
        {
            try
            {
                new OrderBL().Add(order);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateClient(OrderDTO order)
        {
            try
            {
                new OrderBL().Update(order);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }


        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteClient(int id)
        {
            try
            {
                new OrderBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
