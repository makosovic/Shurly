using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Shurly.Core.Security;

namespace Shurly.Core.WebApi.Attributes
{
    public class BasicAuth : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            AuthenticationHeaderValue authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase)
                    && !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    string[] credArray = GetCredentials(authHeader);
                    string accountId = credArray[0];
                    string password = credArray[1];

                    ISecurityService securityService = new SecurityService();
                    if (securityService.Authenticate(accountId, password))
                    {
                        IPrincipal currentPrincipal = new GenericPrincipal(new GenericIdentity(accountId), null);
                        Thread.CurrentPrincipal = currentPrincipal;
                        return;
                    }
                }
            }

            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }

        private string[] GetCredentials(AuthenticationHeaderValue authHeader)
        {
            string rawCred = authHeader.Parameter;
            Encoding encoding = Encoding.GetEncoding("ISO-8859-1");
            string cred = encoding.GetString(Convert.FromBase64String(rawCred));
            string[] credArray = cred.Split(':');
            return credArray;
        }
    }
}