using System;
using System.Net;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Core.Interfaces.Excluded
{
    public interface IGenericApiClient<T> where T : ISerializer
    {
        void SetBaseUrl( Uri uri );
        void SetBearerToken( string token );
        HttpStatusCode Patch<T>( string uri, T body );
        ( HttpStatusCode, T ) Get<T>( string uri );
        HttpStatusCode Post<T>( string uri, T body );
    }
}