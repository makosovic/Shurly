namespace Shurly.Core.WebApi.Models
{
    public class RegisterRequestBody
    {
        public virtual string Url { get; set; }
        public virtual int? RedirectType { get; set; }
    }
}
