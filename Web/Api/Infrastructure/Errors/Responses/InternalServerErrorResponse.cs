using System.Collections.Generic;
using Nancy;
using Web.Api.Infrastructure.Siren.Siren;

namespace Web.Api.Infrastructure.Errors.Responses
{
    public class InternalServerErrorResponse: Entity
    {
        public InternalServerErrorResponse(NancyContext context) : base(context.Request.Url, new[] { "error", "internal-server-error" })
        {
            Properties = new Dictionary<string, object> { { "Error Message", "There was an internal server error..." } };
        }
    }
}