namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IWebClientAdapter
    {
        Result DownloadFile( string url, string destination );
    }
}