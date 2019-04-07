using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using ColorMixERP.Helpers;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Logging;
using ColorMixERP.Models;
using Newtonsoft.Json;

namespace ColorMixERP.Controllers
{
    public class SalesController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/Sales")]
        public HttpResponseMessage GetSales(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<SaleCommand>(query);
                int pagesCount = 0;
                var data = new SaleBL().GetSales(cmd, ref pagesCount);
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
        [Route("api/Sales/{id}")]
        public HttpResponseMessage GetSale(int id)
        {
            try
            {
                var data = new SaleBL().GetSale(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("api/Sales/CurrencyRate")]
        public HttpResponseMessage CurrencyRate()
        {
            try
            {
                var data = new SaleBL().GetLatestCurrencyRate();
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
        [Route("api/Sales")]
        public HttpResponseMessage Add(SaleDTO dto)
        {
            try
            {
                new SaleBL().Add(dto);
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
        [Route("api/Sales")]
        public HttpResponseMessage Update(SaleDTO sale)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new SaleBL().Update(sale, userId);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("api/Sales/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                new SaleBL().Delete(id);
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
