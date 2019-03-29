using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Logging;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Models;
using Newtonsoft.Json;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Controllers
{
    public class ExpensesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetExpenses(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<PaginationDTO>(query);
                int pagesCount = 0;
                var data = new ExpenseBL().GetExpenses(cmd, ref pagesCount);
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
        public HttpResponseMessage GetExpense(int id)
        {
            try
            {
                var data = new ExpenseBL().GetExpense(id);
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
        public HttpResponseMessage Add(int id, ExpenseDTO expense)
        {
            try
            { /// ID =  userId (MUST BE!!!)
                new ExpenseBL().Add(id,expense);
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
        public HttpResponseMessage Update(ExpenseDTO expense)
        {
            try
            {
                new ExpenseBL().Update(expense);
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
                new ExpenseBL().Delete(id);
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