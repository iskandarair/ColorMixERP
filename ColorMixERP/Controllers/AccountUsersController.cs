using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.AuthorizationEntities;

namespace ColorMixERP.Controllers
{
    public class AccountUsersController : ApiController
    {


        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetAccountUsers()
        {
            var data = new UserBL().GetAccountUsers();
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetUserById(int id)
        {
            var data = new UserBL().GetAccountUser(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }


        [Authorize]
        [HttpPost]
        public HttpResponseMessage AddUser(AccountUserDTO user)
        {
            try
            {
                new UserBL().Add(user);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPut]
        public HttpResponseMessage UpdateUser(AccountUserDTO user)
        {
            try
            {
                new UserBL().Update(user);
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
                new UserBL().Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, false);
            }
        }

        [Authorize]
        [HttpPost]
        public HttpResponseMessage Add(int id, Expense expense)
        {
           return new ExpensesController().Add(id, expense);
        }
    }
}
