using System;
using System.Web.Http;
using Newtonsoft.Json;
using Shurly.Core.Enums;
using Shurly.Core.Helpers;
using Shurly.Core.Models;
using Shurly.Core.Persistance;
using Shurly.Core.Security;
using Shurly.Core.WebApi.Attributes;
using Shurly.Core.WebApi.Models;

namespace Shurly.Web.Controllers
{
    public class ShurlyController : ApiController
    {
        [HttpPost]
        // POST account
        public IHttpActionResult Account([FromBody] AccountRequestBody requestBody)
        {
            if (requestBody == null || string.IsNullOrEmpty(requestBody.AccountId))
            {
                return BadRequest("Parameter AccountId is required.");
            }

            IAccountStore accountStore = new CacheStore();

            try
            {
                var account = accountStore.CreateAccount(requestBody.AccountId,
                    RandomPassword.GeneratePassword(Characters.AlphaNumeric));
                return
                    Ok(new AccountResponseBody
                    {
                        Success = true,
                        Description = "Your account has been created.",
                        Password = account.Password
                    });
            }
            catch (ApplicationException ex)
            {
                return Ok(new AccountResponseBody {Success = false, Description = ex.Message});
            }
        }

        [HttpPost]
        [BasicAuth]
        // POST register
        public IHttpActionResult Register([FromBody] RegisterRequestBody requestBody)
        {
            if (requestBody == null || string.IsNullOrEmpty(requestBody.Url))
            {
                return BadRequest("Parameter url is required.");
            }

            IShurlyStore shurlyStore = new CacheStore();

            try
            {
                IShurly shurly;

                if (requestBody.RedirectType == null)
                {
                    shurly = shurlyStore.Register(requestBody.Url, User.Identity.Name);
                }
                else
                {
                    shurly = shurlyStore.Register(requestBody.Url, User.Identity.Name,
                        (RedirectType) requestBody.RedirectType);
                }

                return Ok(new RegisterResponseBody {ShortUrl = shurly.ShortUrl});
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [BasicAuth]
        // GET statistic/5
        public IHttpActionResult Statistic(string accountId)
        {
            IShurlyStore shurlyStore = new CacheStore();

            try
            {
                return Ok(shurlyStore.GetStatistics(accountId));
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        // GET fG3da2
        public IHttpActionResult Get(string shortUrl)
        {
            IShurlyStore shurlyStore = new CacheStore();

            try
            {
                var shurly = shurlyStore.GetShurlyByShortUrl(shortUrl);
                shurlyStore.LogRedirect(shortUrl);
                return new ShurlyRedirectActionResult(Request, shurly.Url, shurly.RedirectType);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}