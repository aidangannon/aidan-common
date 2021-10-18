using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IApiClientFactory : IFactory
    {
        IApiClient Factory( Uri uri, string token );
    }
}