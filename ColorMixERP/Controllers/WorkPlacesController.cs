using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Logging;
using ColorMixERP.Models;
using Newtonsoft.Json;

namespace ColorMixERP.Controllers
{
    public class WorkPlacesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetWorkPlaces(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<PaginationDTO>(query);
                int pagesCount = 0;
                var data = new WorkPlaceBL().GetWorkPlaces(cmd,ref pagesCount);
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
        public HttpResponseMessage GetWorkPlace(int id)
        {
            try
            {
                var data = new WorkPlaceBL().GetWorkPlace(id);
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
        public HttpResponseMessage AddWorkPlace(WorkPlaceDTO workPlace)
        {
            try
            {
                new WorkPlaceBL().Add(workPlace);
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
        public HttpResponseMessage UpdateWorkPlace(WorkPlaceDTO workPlace)
        {
            try
            {
                new WorkPlaceBL().Update(workPlace);
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
        public HttpResponseMessage UpdateWorkPlace(int id)
        {
            try
            {
                new WorkPlaceBL().Delete(id);
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
        [ActionName("ProductStocks")]
        public HttpResponseMessage Add(int id, ProductStockDTO stock)
        {
            try
            {
                new ProductStockBL().Add(stock);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/WorkPlaces/{id}/ProductStocks")]
        public HttpResponseMessage GetProductStockDtosByWp(int id, ProductStockCommand cmd)
        {
            try
            {
                int pagesCount = 0;
                var data = new ProductStockBL().GetProductStockDtosByWp(id,cmd, ref pagesCount);
                var result = Request.CreateResponse(HttpStatusCode.OK, data);
                result.Headers.Add(Consts.PAGES_COUNT, pagesCount.ToString());
                return result;
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
    
    }
}
