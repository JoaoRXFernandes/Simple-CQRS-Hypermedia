using System;
using Infrastructure.CQRS.Domain.Exceptions;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.ModelBinding;
using Web.Api.Infrastructure.Errors.Responses;
using Web.Api.Infrastructure.Exceptions;

namespace Web.Api.Infrastructure.Errors
{
    public class ErrorHandler : IStatusCodeHandler
    {
        private readonly IResponseFormatterFactory _responseFormatterFactory;

        public ErrorHandler(IResponseFormatterFactory responseFormatterFactory)
        {
            _responseFormatterFactory = responseFormatterFactory;
        }

        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError || statusCode == HttpStatusCode.MethodNotAllowed;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            var formatter = _responseFormatterFactory.Create(context);

            context.Response.StatusCode = HttpStatusCode.OK;

            object errorObject;
            context.Items.TryGetValue(NancyEngine.ERROR_EXCEPTION, out errorObject);
            var exception = errorObject as Exception;

            if (exception != null)
            {
                var bindingException = exception.InnerException as ModelBindingException;
                if (bindingException != null)
                {
                    context.Response = formatter.AsJson(new BadRequestResponse(context, bindingException.PropertyBindingExceptions)).WithContentType("application/vnd.siren+json; charset=utf-8");
                    return;
                }

                var validationException = exception.InnerException as ValidationException;
                if (validationException != null)
                {
                    context.Response = formatter.AsJson(validationException.Response).WithContentType("application/vnd.siren+json; charset=utf-8");
                    return;
                }

                var domainException = exception.InnerException as DomainException;
                if (domainException != null)
                {
                    context.Response = formatter.AsJson(new BadRequestResponse(context, domainException.Message)).WithContentType("application/vnd.siren+json; charset=utf-8");
                    return;
                }
            }

            context.Response = formatter.AsJson(new InternalServerErrorResponse(context)).WithContentType("application/vnd.siren+json; charset=utf-8");
        }
    }
}