using NUnit.Framework;

namespace Aidan.Common.Tests.Integration.TestEventStatePollingServiceTests
{

    public class When_State_Changes_Once : Given_A_TestEventStatePollingService
    {
        private int _initialState;

        protected override void When( )
        {
            _initialState = EventState.Value;
            SUT.DoWork( );
            SUT.Cancel( );
        }

        [ Test ]
        public void Then_New_State_Increased_By_1( )
        {
            Assert.AreNotEqual( _initialState, EventState.Value );
            Assert.AreEqual( 1, EventState.Value );
        }

        [ Test ]
        public void Then_Handler_Is_Called_1( ) { Assert.AreEqual( 1, HandlerCalled ); }
    }
}