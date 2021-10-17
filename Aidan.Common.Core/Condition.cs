using System;

namespace Aidan.Common.Core
{
    public abstract class Condition<T> where T : Exception, new( )
    {
        public void Evaluate( bool predicate )
        {
            if( !predicate ) throw new T( );
        }
    }
}