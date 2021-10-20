using System.IO;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class WindowsFileAdapter : IFileAdapter
    {
        public Result Exists( string path ) =>
            File.Exists( path ) ? Result.Success( ) : Result.Error( $"file at {path} was not found" ) ;
    }
}