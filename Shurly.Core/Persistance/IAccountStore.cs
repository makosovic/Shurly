using Shurly.Core.Security;

namespace Shurly.Core.Persistance
{
    public interface IAccountStore
    {
        IAccount FindAccountById(string accountId);
        IAccount CreateAccount(string accountId, string password);
    }
}