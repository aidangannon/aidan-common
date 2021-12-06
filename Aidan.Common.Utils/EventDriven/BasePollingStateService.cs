using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BasePollingStateService<TStateType> : BasePollingService
    {
        protected BasePollingStateService( IEventState<TStateType> eventState, Func<TStateType> workToBeDone ) : base(
            ( ) => eventState.Value = workToBeDone( ), 1000 )
        {
        }
    }
}