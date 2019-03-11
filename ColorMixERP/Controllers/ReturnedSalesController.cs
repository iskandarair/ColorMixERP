using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Logging;
using ColorMixERP.Models;
using Newtonsoft.Json;

namespace ColorMixERP.Controllers
{
    public class ReturnedSalesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<PaginationDTO>(query);
                int pagesCount = 0;
                var data = new ReturnedSaleBL().GetReturnedSales(cmd, ref pagesCount);
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
        public HttpResponseMessage Get(int id)
        {
            try
            {
                var data = new ReturnedSaleBL().GetReturnedSale(id);
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
        public HttpResponseMessage Add(ReturnedSaleDTO element)
        {
            try
            {
                new ReturnedSaleBL().Add(element);
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
        public HttpResponseMessage Update(ReturnedSaleDTO dto)
        {
            try
            {
                new ReturnedSaleBL().Update(dto);
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
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                new ReturnedSaleBL().Delete(id);
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
