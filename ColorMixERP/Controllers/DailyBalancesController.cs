using ColorMixERP.Server.BL;
using ColorMixERP.Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Entities.DTO;
using Newtonsoft.Json;
using ColorMixERP.Models;
using ColorMixERP.Server.Entities.Pagination;

namespace ColorMixERP.Controllers
{
    public class DailyBalancesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetCategories(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<DailyBalanceCommand>(query);
                int pagesCount = 0;
                var data = new DailyBalanceBL().GetDailyBalances(cmd, ref pagesCount);
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
        public HttpResponseMessage GetCategory(int id)
        {
            try
            {
                var data = new DailyBalanceBL().GetDailyBalance(id);
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
        public HttpResponseMessage AddCategory(DailyBalanceDTO dto)
        {
            try
            {
                new DailyBalanceBL().Add(dto);
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
        public HttpResponseMessage UpdateCategory(DailyBalanceDTO dto)
        {
            try
            {
                new DailyBalanceBL().Update(dto);
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
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                new DailyBalanceBL().Delete(id);
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
