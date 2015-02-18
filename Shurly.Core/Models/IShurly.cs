using Shurly.Core.Enums;

namespace Shurly.Core.Models
{
    public interface IShurly
    {
        long Id { get; }
        string Url { get; }
        string ShortUrl { get; }
        RedirectType RedirectType { get; }
        string OwnerId { get; }
        int Visits { get; }
        void LogVisit();
        string CreateShortUrl(long number);
    }
}