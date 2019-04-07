using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Models;
using Newtonsoft.Json;

namespace ColorMixERP.Controllers
{
    public class ClientsController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/Clients")]
        public HttpResponseMessage GetClients(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<ClientCommand>(query);
                int pagesCount = 0;
                var data = new ClientBL().GetClients(cmd, ref pagesCount);
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
        [Route("api/Clients/{id}")]
        public HttpResponseMessage GetClientById(int id)
        {
            try
            {
                var data = new ClientBL().GetClient(id);
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
        [Route("api/Clients")]
        public HttpResponseMessage AddUser(ClientDTO client)
        {
            try
            {
                new ClientBL().Add(client);
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
        [Route("api/Clients")]
        public HttpResponseMessage UpdateClient(ClientDTO client)
        {
            try
            {
                new ClientBL().Update(client);
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
        [Route("api/Clients/{id}")]
        public HttpResponseMessage DeleteClient(int id)
        {
            try
            {
                new ClientBL().Delete(id);
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
