using Shurly.Core.Enums;
using Shurly.Core.Helpers;

namespace Shurly.Core.Models
{
    public class Shurly : IShurly
    {
        private readonly long _id;
        private readonly string _url;
        private readonly string _shortUrl;
        private readonly RedirectType _redirectType;
        private readonly string _ownerId;
        private int _visits;

        public long Id { get { return _id; } }
        public string Url { get { return _url; } }
        public string ShortUrl { get { return _shortUrl; } }
        public RedirectType RedirectType { get { return _redirectType; } }
        public string OwnerId { get { return _ownerId; } }
        public int Visits { get { return _visits;} }

        public Shurly(long id, string url, string ownerId)
            : this(id, url, ownerId, RedirectType.Temporary)
        {
        }

        public Shurly(long id, string url, string ownerId, RedirectType redirectType)
        {
            _id = id;
            _url = url;
            _ownerId = ownerId;
            _redirectType = redirectType;
            _visits = 0;
            _shortUrl = CreateShortUrl(id);
        }

        public void LogVisit()
        {
            _visits++;
        }

        public string CreateShortUrl(long number)
        {
            return CreateShortUrl(number, Characters.AlphaNumeric);
        }

        private string CreateShortUrl(long number, string chars)
        {
            return CustomNumberBase.Encode(number, chars.ToCharArray());
        }
    }
}
