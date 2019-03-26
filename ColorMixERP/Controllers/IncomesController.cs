using ColorMixERP.Models;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ColorMixERP.Controllers
{
    public class IncomesController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/Incomes/ProductArrivals")]
        public HttpResponseMessage GetProductArrivals(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<IncomeCommand>(query);
                int pagesCount = 0;
                var data = new IncomeBL().GetProductArrivals(cmd, ref pagesCount);
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


        [Route("api/Incomes/")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetIncomes(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<IncomeCommand>(query);
                int pagesCount = 0;
                var data = new IncomeBL().GetIncomes(cmd, ref pagesCount);
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
        public HttpResponseMessage GetIncomes(int[] ids)
        {
            try
            {
                var data = new IncomeBL().GetIncomes(ids);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
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
        public HttpResponseMessage GetIncomeProducts(int id)
        {
            try
            {
                var data = new IncomeBL().GetIncomeProducts(id);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage GetIncomeProductById(int id)
        {
            try
            {
                var data = new IncomeBL().GetIncomeProductById(id);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddIncome(IncomeDTO dto)
        {
            try
            {
                new IncomeBL().AddIncome(dto);
                var result = Request.CreateResponse(HttpStatusCode.OK, true);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateIncome(IncomeProductDTO dto)
        {
            try
            {
                new IncomeBL().Update(dto);
                var result = Request.CreateResponse(HttpStatusCode.OK, true);
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
