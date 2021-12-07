using System.Collections.Generic;
using System.Linq;
using Aidan.Common.Core.Interfaces.Excluded;
using Aidan.Common.TestModule;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aidan.Common.Tests.Integration.Bootstrap.BindingTests
{
    public class When_Initializables_Are_Binded : Given_A_Module_Is_Binded
    {
        private IEnumerable<IInitialisable> _initialisables;

        protected override void When( )
        {
            _initialisables = ServiceProvider.GetServices<IInitialisable>( );
        }

        [ Test ]
        public void Then_Singleton_Initializer_Should_Exist( )
        {
            Assert.DoesNotThrow( ( ) => _initialisables.First( x => x.GetType( ) == typeof( SingletonInitializer ) ) );
        }
        
        [ Test ]
        public void Then_Transient_Initializer_Should_Exist( )
        {
            Assert.DoesNotThrow( ( ) => _initialisables.First( x => x.GetType( ) == typeof( TransientInitializer ) ) );
        }
        
        [ Test ]
        public void Then_Scoped_Initializer_Should_Exist( )
        {
            Assert.DoesNotThrow( ( ) => _initialisables.First( x => x.GetType( ) == typeof( ScopedInitializer ) ) );
        }
        
        [ Test ]
        public void Then_Singleton_Initializer_Should_Be_Singleton( )
        {
            Assert.AreEqual( ServiceLifetime.Singleton,
                SUT.First( x => x.ImplementationType == typeof( SingletonInitializer ) ).Lifetime );
        }
        
        [ Test ]
        public void Then_Transient_Initializer_Should_Be_Singleton( )
        {
            Assert.AreEqual( ServiceLifetime.Transient,
                SUT.First( x => x.ImplementationType == typeof( TransientInitializer ) ).Lifetime );
        }
        
        [ Test ]
        public void Then_Scoped_Initializer_Should_Be_Scoped( )
        {
            Assert.AreEqual( ServiceLifetime.Scoped,
                SUT.First( x => x.ImplementationType == typeof( ScopedInitializer ) ).Lifetime );
        }
    }
}