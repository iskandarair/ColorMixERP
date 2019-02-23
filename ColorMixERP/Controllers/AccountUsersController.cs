using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Logging;

namespace ColorMixERP.Controllers
{
    public class AccountUsersController : ApiController
    {
        [Authorize]
        [HttpGet]
        [Route("api/AccountUsers/{id}/Expenses")]
        public HttpResponseMessage GetUserExpenses(int id)
        {
            try
            {
                var data = new ExpenseBL().GetUserExpenses(id);
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
        public HttpResponseMessage GetAccountUsers()
        {
            try
            {
                var data = new UserBL().GetAccountUsers();
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
        public HttpResponseMessage GetUserById(int id)
        {
            try
            {
                var data = new UserBL().GetAccountUser(id);
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
        public HttpResponseMessage AddUser(AccountUserDTO user)
        {
            try
            {
                new UserBL().Add(user);
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
        public HttpResponseMessage UpdateUser(AccountUserDTO user)
        {
            try
            {
                new UserBL().Update(user);
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
        [Route("api/AccountUsers/{id}/ChangeUserPassowrd")]
        public HttpResponseMessage UpdateUserPassowrd(int id, AccountUserDTO user)
        {
            try
            {
                new UserBL().UpdatePassword(user);
                LogManager.Instance.Info($"User {user.Name} {user.Surname} (User Id - {user.Id}) - Password updated");
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
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                new UserBL().Delete(id);
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
        public HttpResponseMessage Add(int id, Expense expense)
        {
            try
            {
                return new ExpensesController().Add(id, expense);
            }
            catch (Exception ex)
            {
                LogManager.Instance.Error(ex);
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}
