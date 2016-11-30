using Web.Api.Infrastructure.Siren.Siren;

namespace Web.Api.Infrastructure.Exceptions
{
    public class ValidationException : HypermediaEngineException
    {
        public ValidationException(Entity response) : base(response)
        {
        }
    }
}