using Web.Api.Infrastructure.Requests.Actions;

namespace Web.Api.Infrastructure.Siren.Actions
{
    public interface IWithAction<T> where T : ApiAction
    {
    }
}