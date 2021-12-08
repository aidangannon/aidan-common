using NSubstitute;
using NUnit.Framework;

namespace Aidan.Common.Tests.Unit
{
    [ TestFixture( 2, 3 ) ]
    [ TestFixture( 3, 4 ) ]
    [ TestFixture( 4, 5 ) ]
    public class When_Condition_Is_Valid : Given_A_Condition
    {
        private readonly int _prev;
        private readonly int _current;

        public When_Condition_Is_Valid( int prev, int current )
        {
            _prev = prev;
            _current = current;
        }
        
        protected override void When( )
        {
            MockTestEventState.Value.Returns( _current );
            MockTestEventState.PreviousValue.Returns( _prev );
            SUT.Evaluate( );
        }

        [ Test ]
        public void Then_Good_Handler_Is_Run( )
        {
            MockHandler.Received( 1 ).Good( );
        }
    }
}