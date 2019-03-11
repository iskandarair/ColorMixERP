using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Logging;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Models;
using Newtonsoft.Json;

namespace ColorMixERP.Controllers
{
    public class OrdersController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/orders/{id}/ReturnedSales")]
        public HttpResponseMessage GetOrderReturnedSale(int id)
        {
            try
            {
                var data = new ReturnedSaleBL().GetOrderReturnedSale(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/orders/{id}/DebtCovers")]
        public HttpResponseMessage GetDebtCovers(int id)
        {
            try
            {
                var data = new DebtCoverBL().GetOrderDebtCovers(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/orders/")]
        public HttpResponseMessage GetOrders(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<OrderCommand>(query);
                int pagesCount = 0;
                var data = new OrderBL().GetClientOrders(cmd, ref pagesCount);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                result.Headers.Add(Consts.PAGES_COUNT, pagesCount.ToString());
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/orders/{id}")]
        public HttpResponseMessage GetOrder(int id)
        {
            try
            {
                var data = new OrderBL().GetClientOrder(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/orders/")]
        public HttpResponseMessage Add(OrderDTO order)
        {
            try
            {
                new OrderBL().Add(order);
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
        [Route("api/orders/")]
        public HttpResponseMessage UpdateClient(OrderDTO order)
        {
            try
            {
                new OrderBL().Update(order);
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
        [Route("api/orders/{id}")]
        public HttpResponseMessage DeleteClient(int id)
        {
            try
            {
                new OrderBL().Delete(id);
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
