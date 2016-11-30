
namespace Web.Api.Infrastructure.Requests
{
    public abstract class ApiRequest
    {
        public string Title { get; private set; }
        public string Href { get; private set; }
        
        protected ApiRequest(string title, string href)
        {
            Title = title;
            Href = href;
        }
    }
}