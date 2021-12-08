using System.Linq;
using Aidan.Common.TestModule.Core.Interfaces.Contract;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aidan.Common.Tests.Integration.Bootstrap.BindingTests
{
    public class When_Conditions_Are_Bound : Given_A_Module_Is_Binded
    {
        [ Test ]
        public void Then_Test_Condition_Is_Singleton( )
        {
            Assert.AreEqual( ServiceLifetime.Singleton, SUT.First( x => x.ServiceType == typeof( ITestCondition ) ).Lifetime );
        }
    }
}