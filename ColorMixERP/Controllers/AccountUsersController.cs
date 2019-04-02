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
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Models;
using Newtonsoft.Json;

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
        [Route("api/AccountUsers")]
        public HttpResponseMessage GetAccountUsers(string query)
        {
            try
            {
                var cmd = JsonConvert.DeserializeObject<UserCommand>(query);
                int pagesCount = 0;
                var data = new UserBL().GetAccountUsers(cmd, ref pagesCount);
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
        [Route("api/AccountUsers/{id}")]
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
        [Route("api/AccountUsers")]
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
        [Route("api/AccountUsers")]
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
        [Route("api/AccountUsers/{id}")]
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

       // [Authorize]
       // [HttpPost]
       // [Route("api/AccountUsers/{id}")]
       // public HttpResponseMessage Add(int id, ExpenseDTO expense)
       // {
       //     try
       //     {
       //         new ExpenseBL().Add(expense.UserId, expense);
       //         return Request.CreateResponse(HttpStatusCode.OK, true);
       //     }
       //     catch (Exception ex)
       //     {
       //         LogManager.Instance.Error(ex);
       //         return Request.CreateResponse(HttpStatusCode.InternalServerError);
       //     }
       // }
    }
}
