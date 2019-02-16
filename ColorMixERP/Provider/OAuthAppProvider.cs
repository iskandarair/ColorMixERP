using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Threading.Tasks;
using ColorMixERP.Server.BL;
using System.Security.Claims;
using Microsoft.Owin.Security;
using ColorMixERP.Server.Entities.AuthorizationEntities;

namespace ColorMixERP.Provider
{
    public class OAuthAppProvider : OAuthAuthorizationServerProvider
    {
        public static readonly string USER_ID = "userId";
        public static readonly string WORKPLACE_ID = "workPlaceId";
        public static readonly string FULL_NAME = "fullName";
        public static readonly string POSITION_ROLE = "positiionRoleId";

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var username = context.UserName;
                var password = context.Password;
                var userService = new UserBL();
                User user = userService.GetUserByCredentials(username, password);
                if (user != null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim("UserID", user.Id.ToString())
                    };

                    ClaimsIdentity oAutIdentity = new ClaimsIdentity(claims, Startup.OAuthOptions.AuthenticationType);
                    var authProps = new AuthenticationProperties(new Dictionary<string, string>()
                    {
                        {USER_ID, user.Id.ToString()},
                        {FULL_NAME, user.FullName},
                        {WORKPLACE_ID, user.WorkplaceId},
                        {POSITION_ROLE,user.PosotionRoleId}
                    });
                    context.Validated(new AuthenticationTicket(oAutIdentity, authProps));
                }
                else
                {
                    context.SetError("invalid_grant", "Error");
                }
            });
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        } 
    }
}