using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;

namespace Shurly.SelfHost.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Index()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Shurly.SelfHost.Content.Index.html";
            string index;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                index = reader.ReadToEnd();
            }

            var response = new HttpResponseMessage();
            response.Content = new StringContent(index);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }

        [HttpGet]
        public HttpResponseMessage Help()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "Shurly.SelfHost.Content.Help.html";
            string help;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                help = reader.ReadToEnd();
            }

            var response = new HttpResponseMessage();
            response.Content = new StringContent(help);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }
    }
}
