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

namespace ColorMixERP.Controllers
{
    public class ClientsController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetClients()
        {
            try
            {
                var data = new ClientBL().GetClients();
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
