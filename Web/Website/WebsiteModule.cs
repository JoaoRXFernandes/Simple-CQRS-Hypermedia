using Nancy;

namespace Web.Website
{
    public class WebsiteModule : NancyModule
    {
        public WebsiteModule()
        {
            Get["/"] = _ => View["Index"];
        }
    }
}
