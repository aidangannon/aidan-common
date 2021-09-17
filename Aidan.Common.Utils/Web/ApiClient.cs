using System;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Web
{
    public class ApiClient : ApiClientBase, IApiClient
    {
        public ApiClient( Uri uri, string token, ISerializer serializer, IHttpClient httpClient ) : base( uri, token,
            serializer, httpClient )
        {
        }
    }
}