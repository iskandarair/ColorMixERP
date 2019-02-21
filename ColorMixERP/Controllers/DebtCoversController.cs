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
    public class DebtCoversController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            var data = new DebtCoverBL().GetDebtCovers();
            return Request.CreateResponse(HttpStatusCode.OK,data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            var data = new DebtCoverBL().GetDebtCover(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(DebtCoverDTO element)
        {
            try
            {
                new DebtCoverBL().Add(element);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update(DebtCoverDTO dto)
        {
            try
            {
                new DebtCoverBL().Update(dto);
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
                new DebtCoverBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

    }
}
