using System;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Excluded;
using Aidan.Common.DependencyInjection;
using Aidan.Common.TestModule;
using Aidan.Common.TestModule.Core;
using Aidan.Common.Utils;
using Aidan.Common.Utils.Test;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aidan.Common.Tests.Integration.Bootstrap
{
    public abstract class Given_A_Module_Is_Binded : GivenWhenThen<IServiceCollection>
    {
        protected ServiceProvider ServiceProvider;

        protected override void Given( )
        {
            SUT = new ServiceCollection( )
                .BindServices( new Action[]
                {
                    TestModuleInitializer.Initialize,
                    TestModuleCoreInitializer.Initialize,
                }, "Aidan.Common.TestModule" )
                .BindServices( new Action[]
                {
                    CommonInitializer.Initialize,
                    CommonUtilsInitializer.Initialize
                }, "Aidan.Common" );
            ServiceProvider = SUT.BuildServiceProvider( );
            foreach( var initialisable in ServiceProvider.GetServices<IInitialisable>( ) )
            {
                initialisable.Initialize( );
            }
        }

        [ Test ]
        public void Then_Container_Is_Not_Empty( )
        {
            CollectionAssert.IsNotEmpty( SUT );
        }
    }
}