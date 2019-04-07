using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Logging;
using ColorMixERP.Server.Entities.Pagination;
using Newtonsoft.Json;
using ColorMixERP.Models;

namespace ColorMixERP.Controllers
{
    public class DebtorCreditorsController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/DebtorCreditors")]
        public HttpResponseMessage Get(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<DCCommand>(query);
                int pagesCount = 0;
                var data = new DebtorCreditorsBL().Get(cmd, ref pagesCount);
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
        [Route("api/DebtorCreditors/{id}")]
        public HttpResponseMessage GetUserById(int id)
        {
            try
            {
                var data = new DebtorCreditorsBL().GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
        
        [Authorize]
        [HttpPut]
        [Route("api/DebtorCreditors")]
        public HttpResponseMessage Update(DebtorCreditorDTO dto)
        {
            try
            {
                new DebtorCreditorsBL().Update(dto);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
        
        [Authorize]
        [HttpPost]
        [Route("api/DebtorCreditors")]
        public HttpResponseMessage Add(DebtorCreditorDTO dto)
        {
            try
            {
                new DebtorCreditorsBL().Add(dto);
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
