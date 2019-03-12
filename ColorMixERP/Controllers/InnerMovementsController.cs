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
using System.Security.Claims;
using ColorMixERP.Helpers;

namespace ColorMixERP.Controllers
{
    public class InnerMovementsController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetInnerMovements(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<InnerMovementCommand>(query);
                int pagesCount = 0;
                var data = new InnerMovementBL().GetInnerMovementDtos(cmd, ref pagesCount);
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
        public HttpResponseMessage GetInnerMovementDto(int id)
        {
            try
            {
                var data = new InnerMovementBL().GetInnerMovementDto(id);
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
        [Route("api/InnerMovements")]
        public HttpResponseMessage Add(InnerMovementDTO dto)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new InnerMovementBL().Add(dto, userId);
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
        [Route("api/InnerMovements/Range")]
        public HttpResponseMessage Add(InnerMovementDTO[] dto)
        {
            try
            {
                var userId = AuthHelper.GetUserIdFromClaims(User.Identity as ClaimsIdentity);
                new InnerMovementBL().Add(dto.ToList(), userId);
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
        public HttpResponseMessage Update(InnerMovementDTO dto)
        {
            try
            {
                new InnerMovementBL().Update(dto);
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
                new InnerMovementBL().Delete(id);
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
