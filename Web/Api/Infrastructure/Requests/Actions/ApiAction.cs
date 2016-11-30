namespace Web.Api.Infrastructure.Requests.Actions
{
    public abstract class ApiAction : ApiRequest
    {
        public string Method { get; private set; }

        protected ApiAction(string title, string method, string href) : base(title, href)
        {
            Method = method;
        }
    }
}