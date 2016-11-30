using Nancy;
using Nancy.ModelBinding;
using Nancy.Validation;
using Web.Api.Infrastructure.Errors.Responses;
using Web.Api.Infrastructure.Exceptions;

namespace Web.Api.Infrastructure.Extensions
{
    public static class NancyModuleExtensions
    {
        public static T BindAndValidate<T>(this NancyModule module)
        {
            var request = module.Bind<T>();

            var requestValidation = module.Validate(request);
            if (requestValidation.IsValid == false)
                throw new ValidationException(new BadRequestResponse(module.Context, requestValidation.Errors));

            return request;
        }
    }
}