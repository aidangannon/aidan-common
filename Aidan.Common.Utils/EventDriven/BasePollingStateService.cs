using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BasePollingStateService<TState, TStateType> : BasePollingService where TState : IEventState<TStateType>
    {
        protected BasePollingStateService( IEventState<TStateType> eventState, Func<TStateType> workToBeDone ) : base(
            ( ) => eventState.Value = workToBeDone( ), 1000 )
        {
        }
    }
}