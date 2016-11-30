using Nancy;
using Web.Api.Infrastructure.Siren.Siren;

namespace Web.Api.Infrastructure.Siren.Responses
{
    public class SuccessResponse : Entity
    {
        public SuccessResponse(NancyContext context) : base(context.Request.Url.ToString(), new[] { "success" })
        {
        }
    }
}