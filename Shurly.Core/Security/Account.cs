namespace Shurly.Core.Security
{
    public class Account : IAccount
    {
        public Account(string accountId, string password)
        {
            AccountId = accountId;
            Password = password;
        }

        public string AccountId { get; set; }
        public string Password { get; set; }
    }
}
