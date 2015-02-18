using Shurly.Core.Helpers;
using Shurly.Core.Persistance;

namespace Shurly.Core.Security
{
    public class SecurityService : ISecurityService
    {
        private IAccountStore _accountStore;

        public SecurityService()
        {
            _accountStore = new CacheStore();
        }

        public bool Authenticate(string accountId, string password)
        {
            IAccount account = _accountStore.FindAccountById(accountId);
            if (account == null) return false;
            return (string.Equals(account.Password, password));
        }

        public IAccount CreateAccount(string accountId)
        {
            string password = RandomPassword.GeneratePassword(Characters.AlphaNumeric);
            return _accountStore.CreateAccount(accountId, password);
        }
    }
}
