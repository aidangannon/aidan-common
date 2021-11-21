namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IZipAdapter
    {
        Result ExtractToDirectory( string sourceArchiveFileName, string destinationDirectoryName );
    }
}