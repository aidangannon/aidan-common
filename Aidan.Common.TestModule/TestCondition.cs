using Aidan.Common.TestModule.Core.Interfaces.Contract;
using Aidan.Common.Utils.EventDriven;

namespace Aidan.Common.TestModule
{
    public class TestCondition : BaseEventCondition, ITestCondition
    {
        public TestCondition( ITestEventState testEventState ) : base( ( ) =>
            testEventState.Value > 1 && testEventState.PreviousValue != default )
        {
        }
    }
}