using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using Shurly.Core.Enums;
using Shurly.Core.Helpers;
using Shurly.Core.Models;
using Shurly.Core.Persistance;
using Shurly.Core.Security;
using Shurly.Core.WebApi;
using Shurly.Core.WebApi.Attributes;
using Shurly.Core.WebApi.Models;

namespace Shurly.Web.Controllers
{
    public class ShurlyController : ApiController
    {
        [HttpPost]
        // POST api/v1/account
        public IHttpActionResult Account([FromBody]AccountRequestBody requestBody)
        {
            if (requestBody == null || string.IsNullOrEmpty(requestBody.AccountId))
            {
                return Ok(new AccountResponseBody { Success = false, Description = "Parameter AccountId is required." });
            }

            IPersistanceStore persistanceStore = new CacheStore();

            try
            {
                IAccount account = persistanceStore.CreateAccount(requestBody.AccountId, RandomPassword.GeneratePassword(Characters.AlphaNumeric));
                return Ok(new AccountResponseBody { Success = true, Description = "Your account has been created.", Password = account.Password });
            }
            catch (ApplicationException ex)
            {
                return Ok(new AccountResponseBody { Success = false, Description = ex.Message });
            }
        }

        [HttpPost]
        [BasicAuth]
        // POST api/v1/register
        public IHttpActionResult Register([FromBody]RegisterRequestBody requestBody)
        {
            if (requestBody == null || string.IsNullOrEmpty(requestBody.Url))
            {
                return BadRequest("Parameter url is required.");
            }

            IPersistanceStore persistanceStore = new CacheStore();

            try
            {
                IShurly shurly;

                if (requestBody.RedirectType == null)
                {
                    shurly = persistanceStore.Register(requestBody.Url, User.Identity.Name);
                }
                else
                {
                    shurly = persistanceStore.Register(requestBody.Url, User.Identity.Name, (RedirectType)requestBody.RedirectType);
                }

                return Ok(new RegisterResponseBody() { ShortUrl = shurly.ShortUrl });
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [BasicAuth]
        // GET api/v1/statistic/5
        public IHttpActionResult Statistic(int id)
        {
            IPersistanceStore persistanceStore = new CacheStore();

            try
            {
                return Ok(persistanceStore.GetStatistics(User.Identity.Name));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        // GET api/v1/fG3da2
        public IHttpActionResult Get(string shortUrl)
        {
            IPersistanceStore persistanceStore = new CacheStore();

            try
            {
                IShurly shurly = persistanceStore.GetShurlyByShortUrl(shortUrl);
                persistanceStore.LogRedirect(shortUrl);

                ShurlyRedirectResponse response = new ShurlyRedirectResponse(Request, shurly.Url, shurly.RedirectType);
                return response;
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
