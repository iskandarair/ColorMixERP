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
    public class AccountUsersController : ApiController
    {


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAccountUsers()
        {
            var data = new UserDalFacade().GetAccountUsers();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetUserById(int id)
        {
            var data = new UserDalFacade().GetAccountUser(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddUser(AccountUser user)
        {
            try
            {
                new UserDalFacade().Add(user);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateUser(AccountUser user)
        {
            try
            {
                new UserDalFacade().Update(user);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                new UserDalFacade().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }
        /*
        [HttpPost]
        [ActionName("SaveAndDeleteAll")]
        public string SaveAndDeleteAll(int id)
        {
            return $"SaveAndDEleteAll {id}";
        }
         */
    }
}
