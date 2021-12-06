using Aidan.Common.TestModule.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;

namespace Aidan.Common.TestModule;

public class TestEventStatePollingService : BasePollingStateService<int>, ITestEventStatePollingService
{
    public static int NumOfCalls { set; get; }

    public TestEventStatePollingService( ITestEventState eventState ) : base( eventState, () =>
    {
        NumOfCalls++;
        return eventState.Value + 1;
    }, 1 )
    {
    }
}