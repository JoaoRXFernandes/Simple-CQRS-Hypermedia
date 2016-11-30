using Nancy;
using Nancy.Security;

namespace Web.Api.Infrastructure.Modules
{
    public abstract class SecureApiModule : NancyModule
    {
        protected SecureApiModule()
        {
            this.RequiresHttps();
        }
    }
}