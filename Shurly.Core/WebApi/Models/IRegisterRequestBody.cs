namespace Shurly.Core.WebApi.Models
{
    public interface IRegisterRequestBody
    {
        string Url { get; set; }
        int? RedirectType { get; set; }
    }
}
