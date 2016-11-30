using System.Collections.Generic;

namespace Web.Api.Infrastructure.Requests.Links
{
    public abstract class ApiLinkPaged : ApiLink
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        protected ApiLinkPaged(string title, string href, IList<string> rel, int pageNumber = 0, int pageSize = 10) : base(title, href, rel)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}