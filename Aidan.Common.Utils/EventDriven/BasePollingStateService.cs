using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BasePollingStateService<TStateType> : BasePollingService
    {
        protected BasePollingStateService( IEventState<TStateType> eventState, Func<TStateType> workToBeDone, int interval=1000 ) : base(
            ( ) => eventState.Value = workToBeDone( ), interval )
        {
        }
    }
}