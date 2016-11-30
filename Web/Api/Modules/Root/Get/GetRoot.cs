using Web.Api.Infrastructure.Requests.Links;

namespace Web.Api.Modules.Root.Get
{
    public class GetRoot : ApiLink
    {
        public GetRoot() : base("Root", "/api", new[] { "root" })
        {
        }
    }
}