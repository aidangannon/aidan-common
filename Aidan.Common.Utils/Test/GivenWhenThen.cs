using NUnit.Framework;

namespace Aidan.Common.Utils.Test
{
    //TODO: add middleware test class
    public abstract class GivenWhenThen<T>
    {
        protected T SUT;

        [ SetUp ]
        public void SetUp( )
        {
            Given( );
            When( );
        }

        protected virtual void Given( ) { }

        protected virtual void When( ) { }
    }
}