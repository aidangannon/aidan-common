using System.Collections.Generic;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface ICsvAdapter
    {
        ObjectResult<IEnumerable<T>> Read<T>( );
        Result Write<T>( IEnumerable<T> data );
    }
}