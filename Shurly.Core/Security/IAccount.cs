namespace Shurly.Core.Security
{
    public interface IAccount
    {
        string AccountId { get; set; }
        string Password { get; set; }
    }
}