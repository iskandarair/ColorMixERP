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
    public class InnerMovementsController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetInnerMovements()
        {
            var data = new InnerMovementBL().GetInnerMovementDtos();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetInnerMovementDto(int id)
        {
            var data = new InnerMovementBL().GetInnerMovementDto(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(InnerMovementDTO dto)
        {
            try
            {
                new InnerMovementBL().Add(dto);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update(InnerMovementDTO dto)
        {
            try
            {
                new InnerMovementBL().Update(dto);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
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
                new InnerMovementBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
