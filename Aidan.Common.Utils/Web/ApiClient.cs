using System;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Web
{
    public class ApiClient<T> : ApiClientBase<T>, IApiClient<T> where T : ISerializer
    {
        public ApiClient( Uri uri, string token, T serializer, IHttpClient httpClient ) : base( uri, token,
            serializer, httpClient )
        {
        }
    }
}