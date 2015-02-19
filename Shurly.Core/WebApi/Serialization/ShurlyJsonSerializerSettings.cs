using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Shurly.Core.WebApi.Serialization
{
    public class ShurlyJsonSerializerSettings : JsonSerializerSettings
    {
        public ShurlyJsonSerializerSettings()
        {
            Formatting = Formatting.Indented;
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}
