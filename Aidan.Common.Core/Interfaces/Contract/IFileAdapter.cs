namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IFileAdapter
    {
        Result Exists( string path );
        ObjectResult<string> GetFileExtension( string filePath );
        ObjectResult<string> ReadFile( string path );
        Result WriteFile( string path, string content );
    }
}