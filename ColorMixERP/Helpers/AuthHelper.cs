using ColorMixERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace ColorMixERP.Helpers
{
    public static class AuthHelper
    {
        public static int GetUserIdFromClaims(ClaimsIdentity claims)
        {
            var userId = claims.Claims.Where(c => c.Type == Consts.USER_ID).FirstOrDefault().Value;
            return int.Parse(userId);
        }
    }
}