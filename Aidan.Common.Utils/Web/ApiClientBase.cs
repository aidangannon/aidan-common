using System;
using System.Net;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.Web
{
    public abstract class ApiClientBase<TSerializerType> : IGenericApiClient<TSerializerType> where TSerializerType : ISerializer
    {
        protected readonly IHttpClient HttpClient;
        protected readonly TSerializerType Serializer;

        protected ApiClientBase( IHttpClient httpClient, TSerializerType serializer )
        {
            HttpClient = httpClient;
            Serializer = serializer;
        }

        protected ApiClientBase( Uri uri, string token, TSerializerType serializer, IHttpClient httpClient ) : this(
            httpClient, serializer )
        {
            SetBaseUrl( uri );
            SetBearerToken( token );
        }

        public void SetBaseUrl( Uri uri ) { HttpClient.SetBaseUrl( uri ); }
        public void SetBearerToken( string token ) { HttpClient.SetBearerToken( token ); }

        public HttpStatusCode Patch<T>( string uri, T body )
        {
            return HttpClient.Patch( uri, Serializer.Serialize( body ) ).StatusCode;
        }

        public( HttpStatusCode, T ) Get<T>( string uri )
        {
            var result = HttpClient.Get( uri );
            return( result.StatusCode, Serializer.Deserialize<T>( result.Content.ReadAsStringAsync( ).Result ) );
        }

        public HttpStatusCode Post<T>( string uri, T body )
        {
            return HttpClient.Post( uri, Serializer.Serialize( body ) ).StatusCode;
        }
    }
}