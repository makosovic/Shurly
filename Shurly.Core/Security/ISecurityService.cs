
namespace Shurly.Core.Security
{
    public interface ISecurityService
    {
        bool Authenticate(string accountId, string password);
        IAccount CreateAccount(string accountId);
    }
}
