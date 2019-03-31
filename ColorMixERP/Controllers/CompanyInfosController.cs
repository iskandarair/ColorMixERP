using ColorMixERP.Models;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Entities.DTO;

namespace ColorMixERP.Controllers
{
    public class CompanyInfosController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetObjects(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<PaginationDTO>(query);
                int pagesCount = 0;
                var data = new CompanyInfoBL().getObjects(cmd, ref pagesCount);
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
        public HttpResponseMessage GetObject(int id)
        {
            try
            {
                var data = new CompanyInfoBL().getObject(id);
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
        public HttpResponseMessage Add(CompanyInfoDTO dto)
        {
            try
            {
                new CompanyInfoBL().Add(dto);
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
        [Route("api/companyInfo/addOrUpdate")]
        public HttpResponseMessage AddOrUpdate(CompanyInfoDTO dto)
        {
            try
            {
                if(dto.Id > 0)
                {
                    new CompanyInfoBL().Update(dto);
                }
                else
                {
                    new CompanyInfoBL().Add(dto);
                }
                
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
        public HttpResponseMessage Update(CompanyInfoDTO dto)
        {
            try
            {
                new CompanyInfoBL().Update(dto);
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
                new CompanyInfoBL().Delete(id);
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
