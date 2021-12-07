using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.TestModule;
using Aidan.Common.TestModule.Core.Interfaces.Contract;
using Aidan.Common.Utils.Test;
using NSubstitute;

namespace Aidan.Common.Tests.Integration.TestEventStatePollingServiceTests
{

    public class Given_A_TestEventStatePollingService : GivenWhenThen<ITestEventStatePollingService>
    {
        protected TestEventState EventState;
        protected int HandlerCalled;

        protected override void Given( )
        {
            EventState = new TestEventState( );
            SUT = new TestEventStatePollingService( EventState, Substitute.For<ITaskService>( ) );
            EventState.ValueChangedEvent += ( ) => HandlerCalled++;
        }
    }
}