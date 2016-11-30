using System;
using Web.Api.Infrastructure.Siren.Siren;

namespace Web.Api.Infrastructure.Exceptions
{
    public class HypermediaEngineException : Exception
    {
        public Entity Response { get; private set; }

        public HypermediaEngineException(Entity response)
        {
            Response = response;
        }
    }
}