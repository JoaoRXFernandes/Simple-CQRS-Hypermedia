using System;
using Web.Api.Infrastructure.Requests.Actions;
using Action = Web.Api.Infrastructure.Siren.Siren.Action;

namespace Web.Api.Infrastructure.Siren.Actions
{
    public interface IHavingActionOverride<T> : IWithAction<T> where T : ApiAction
    {
        void Apply(Action action);
    }

    public partial class WithAction<T> : IHavingActionOverride<T> where T : ApiAction
    {
        private readonly Action<Action> _actionOverrides;

        private WithAction(Action<Action> actionOverrides)
        {
            _actionOverrides = actionOverrides;
        }

        public static IHavingActionOverride<T> Property(Action<Action> actionOverrides)
        {
            return new WithAction<T>(actionOverrides);
        }

        public void Apply(Action action)
        {
            if (_actionOverrides != null)
                _actionOverrides.Invoke(action);
        }
    }
}