using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Controllers
{
    public class WorkPlacesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkPlaces()
        {
            var data = new WorkPlaceBL().GetWorkPlaces();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkPlace(int id)
        {
            var data = new WorkPlaceBL().GetWorkPlace(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddWorkPlace(WorkPlaceDTO workPlace)
        {
            try
            {
                new WorkPlaceBL().Add(workPlace);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateWorkPlace(WorkPlaceDTO workPlace)
        {
            try
            {
                new WorkPlaceBL().Update(workPlace);
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
                new WorkPlaceBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }


        [Authorize]
        [HttpPost]
        [ActionName("ProductStocks")]
        public HttpResponseMessage Add(int id, ProductStockDTO stock)
        {
            try
            {
                new ProductStockBL().Add(id, stock);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    }
}
