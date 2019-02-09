using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.Config;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;

namespace ColorMixERP.Controllers
{
    public class CategoriesController : ApiController
    {
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetCategories()
        {
            var data = new CategoryBL().GetCategories();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetCategory(int id)
        {
            var data = new CategoryBL().GetCategory(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddCategory(Category category)
        {
            try
            {
                new CategoryBL().Add(category);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateCategory(Category category)
        {
            try
            {
                new CategoryBL().Update(category);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteCategory(int id)
        {
            try
            {
                new CategoryBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

    }
}
