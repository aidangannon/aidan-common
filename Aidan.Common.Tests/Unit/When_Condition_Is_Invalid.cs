using NSubstitute;
using NUnit.Framework;

namespace Aidan.Common.Tests.Unit
{
    [ TestFixture( default( int ), 2 ) ]
    [ TestFixture( default( int ), 3 ) ]
    [ TestFixture( 1, 0 ) ]
    [ TestFixture( 1, 1 ) ]
    public class When_Condition_Is_Invalid : Given_A_Condition
    {
        private readonly int _prev;
        private readonly int _current;

        public When_Condition_Is_Invalid( int prev, int current )
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
        public void Then_Bad_Handler_Is_Run( )
        {
            MockHandler.Received( 1 ).Bad( );
        }
    }
}