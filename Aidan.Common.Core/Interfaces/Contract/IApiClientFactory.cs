using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IApiClientFactory<T> : IFactory where T : ISerializer
    {
        IApiClient<T> Factory( Uri uri, string token );
    }
}