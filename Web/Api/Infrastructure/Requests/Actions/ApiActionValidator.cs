using FluentValidation;

namespace Web.Api.Infrastructure.Requests.Actions
{
    public abstract class ApiActionValidator<T> : AbstractValidator<T> where T : ApiAction
    {
    }
}