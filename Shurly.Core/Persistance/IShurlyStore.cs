using System.Collections;
using System.Collections.Generic;
using Shurly.Core.Enums;
using Shurly.Core.Models;

namespace Shurly.Core.Persistance
{
    public interface IShurlyStore
    {
        IShurly GetShurlyByShortUrl(string shortUrl);
        void LogRedirect(string shortUrl);
        IShurly Register(string url, string accountId);
        IShurly Register(string url, string accountId, RedirectType redirectType);
        IEnumerable<KeyValuePair<string, int>>  GetStatistics(string accountId);
    }
}