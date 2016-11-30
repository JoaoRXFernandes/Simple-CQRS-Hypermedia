using Nancy;
using Nancy.ErrorHandling;
using NotFoundResponse = Web.Api.Infrastructure.Errors.Responses.NotFoundResponse;

namespace Web.Api.Infrastructure.Errors
{
    public class NotFoundErrorHandler : IStatusCodeHandler
    {
        private readonly IResponseFormatterFactory _responseFormatterFactory;

        public NotFoundErrorHandler(IResponseFormatterFactory responseFormatterFactory)
        {
            _responseFormatterFactory = responseFormatterFactory;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.NotFound;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var formatter = _responseFormatterFactory.Create(context);

            context.Response = formatter.AsJson(new NotFoundResponse(context)).WithContentType("application/vnd.siren+json; charset=utf-8");
            context.Response.StatusCode = HttpStatusCode.NotFound;
        }
    }
}