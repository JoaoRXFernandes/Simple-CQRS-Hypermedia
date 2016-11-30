using System.Collections.Generic;

namespace Web.Api.Infrastructure.Requests.Links
{
    public abstract class ApiLink : ApiRequest
    {
        public IList<string> Rel { get; private set; }

        protected ApiLink(string title, string href, IList<string> rel) : base(title, href)
        {
            Rel = rel;
        }
    }
}