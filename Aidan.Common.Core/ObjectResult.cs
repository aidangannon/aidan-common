using System;
using Aidan.Common.Core.Enum;

namespace Aidan.Common.Core
{
    //TODO: provide message param to allow for meaningful error messages

    public class ObjectResult<T> : Result
    {
        [ Obsolete( "prefer object initialization" ) ]
        public ObjectResult( T value, OperationResultEnum status, string msg = null ) : base( status, msg )
        {
            Value = value;
        }

        public ObjectResult( ) { }

        public T Value { get; set; }
    }
}