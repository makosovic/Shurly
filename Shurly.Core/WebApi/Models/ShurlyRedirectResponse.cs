using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Shurly.Core.Enums;

namespace Shurly.Core.WebApi.Models
{
    public class ShurlyRedirectResponse
    {
        private readonly string _location;
        private readonly RedirectType _redirectType;
        private readonly HttpRequestMessage _request;

        public ShurlyRedirectResponse(HttpRequestMessage request, string location, RedirectType redirectType)
        {
            _request = request;
            _location = location;
            _redirectType = redirectType;
        }

        public Task<HttpResponseMessage> ExecuteAsync()
        {
            HttpResponseMessage response;
            switch (_redirectType)
            {
                case RedirectType.Permanent:
                    response = _request.CreateResponse(HttpStatusCode.MovedPermanently);
                    response.Headers.Location = new Uri(_location);
                    return Task.FromResult(response);
                case RedirectType.Temporary:
                    response = _request.CreateResponse(HttpStatusCode.TemporaryRedirect);
                    response.Headers.Location = new Uri(_location);
                    return Task.FromResult(response);
            }
        }
    }
}