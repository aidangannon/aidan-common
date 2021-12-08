using Aidan.Common.TestModule;
using Aidan.Common.TestModule.Core.Interfaces.Contract;
using Aidan.Common.Utils.Test;
using NSubstitute;

namespace Aidan.Common.Tests.Unit
{
    public class Given_A_Condition : GivenWhenThen<ITestCondition>
    {
        protected ITestEventState MockTestEventState;
        protected IHandler MockHandler;

        protected override void Given( )
        {
            MockTestEventState = Substitute.For<ITestEventState>( );
            MockHandler = Substitute.For<IHandler>( );
            SUT = new TestCondition( MockTestEventState );
            SUT.Valid += MockHandler.Good;
            SUT.Invalid += MockHandler.Bad;
        }
    }

    public interface IHandler
    {
        void Good( );
        void Bad( );
    }
}