using System;
using Aidan.Common.Core.Interfaces.Contract;
using NSubstitute;

namespace Aidan.Common.NUnit
{
    public class CommonGivenWhenThen<T> : GivenWhenThen<T> where T : class
    {
        protected ILoggerAdapter<T> MockLogger;

        protected IDateTimeAdapter MockDateTime { get; } = Substitute.For<IDateTimeAdapter>( );

        protected DateTime CurrentTime
        {
            get => MockDateTime.Now( );
            set => MockDateTime
                .Now( )
                .Returns( value );
        }

        protected override void Given( ) { MockLogger = Substitute.For<ILoggerAdapter<T>>( ); }
    }
}