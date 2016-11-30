using System.Collections.Generic;
using Nancy;
using Web.Api.Infrastructure.Siren.Siren;

namespace Web.Api.Infrastructure.Errors.Responses
{
    public class NotFoundResponse: Entity
    {
        public NotFoundResponse(NancyContext context) : base(context.Request.Url, new[] { "error", "not-found" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", "Resource not found!..." } };
        }
    }
}