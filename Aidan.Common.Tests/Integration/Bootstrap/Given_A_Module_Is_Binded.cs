using System;
using Aidan.Common.DependencyInjection;
using Aidan.Common.TestModule;
using Aidan.Common.TestModule.Core;
using Aidan.Common.Utils.Test;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aidan.Common.Tests.Integration.Bootstrap
{
    public abstract class Given_A_Module_Is_Binded : GivenWhenThen<IServiceCollection>
    {
        protected override void Given( )
        {
            SUT = new ServiceCollection( )
                .BindServices( new Action []
                {
                    TestModuleInitializer.Initialize,
                    TestModuleCoreInitializer.Initialize
                }, "Aidan.Common.TestModule" );
        }

        [ Test ]
        public void Then_Container_Is_Not_Empty( )
        {
            CollectionAssert.IsNotEmpty( SUT );
        }
    }
}