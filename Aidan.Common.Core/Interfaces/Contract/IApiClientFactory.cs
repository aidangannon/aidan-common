using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IApiClientFactory : IFactory
    {
        IApiClient<T> Factory<T>( Uri uri, string token ) where T : ISerializer;
    }
}