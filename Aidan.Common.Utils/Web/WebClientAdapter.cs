using System;
using System.Net;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Web
{
    public class WebClientAdapter : IWebClientAdapter
    {
        public Result DownloadFile( string url, string desintation )
        {
            using var webClient = new WebClient( );
            try
            {
                webClient.DownloadFile( url, desintation );
                return Result.Success( );
            }
            catch( Exception e ) when(
                e is ArgumentNullException ||
                e is WebException ||
                e is NotSupportedException
            )
            {
                return Result.Error( e.Message );
            }
        }
    }
}