using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Web.Api.Infrastructure
{
    public sealed class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            NullValueHandling = NullValueHandling.Ignore;

            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = false,
                CamelCaseText = true
            });

            Formatting = Formatting.Indented;
        }
    }
}