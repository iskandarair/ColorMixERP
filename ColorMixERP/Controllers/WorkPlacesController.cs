using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.DAL;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Controllers
{
    public class WorkPlacesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkPlaces()
        {
            var data = new WorkPlaceDalFacade().GetWorkPlaces();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkPlace(int id)
        {
            var data = new WorkPlaceDalFacade().GetWorkPlace(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddWorkPlace(WorkPlace workPlace)
        {
            try
            {
                new WorkPlaceDalFacade().Add(workPlace);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateWorkPlace(WorkPlace workPlace)
        {
            try
            {
                new WorkPlaceDalFacade().Update(workPlace);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpDelete]
        public HttpResponseMessage UpdateWorkPlace(int id)
        {
            try
            {
                new WorkPlaceDalFacade().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
