using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Shurly.Core.Enums;

namespace Shurly.Core.WebApi.Models
{
    public class ShurlyRedirectActionResult : IHttpActionResult
    {
        private readonly string _location;
        private readonly RedirectType _redirectType;
        private readonly HttpRequestMessage _request;

        public ShurlyRedirectActionResult(HttpRequestMessage request, string location, RedirectType redirectType)
        {
            _request = request;
            _location = location;
            _redirectType = redirectType;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response;
            switch (_redirectType)
            {
                case RedirectType.Permanent:
                    response = _request.CreateResponse(HttpStatusCode.MovedPermanently);
                    response.Headers.Location = new Uri(_location);
                    return Task.FromResult(response);
                case RedirectType.Temporary:
                default:
                    response = _request.CreateResponse(HttpStatusCode.TemporaryRedirect);
                    response.Headers.Location = new Uri(_location);
                    return Task.FromResult(response);
            }
        }
    }
}