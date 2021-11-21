using System;
using System.IO.Compression;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class ZipAdapter : IZipAdapter
    {
        public Result ExtractToDirectory( string sourceArchiveFileName, string destinationDirectoryName )
        {
            try
            {
                ZipFile.ExtractToDirectory( sourceArchiveFileName, destinationDirectoryName );
                return Result.Success( );
            }
            catch( Exception e ) when(
                e is System.ArgumentException ||
                e is System.ArgumentNullException ||
                e is System.IO.PathTooLongException ||
                e is System.IO.DirectoryNotFoundException ||
                e is System.IO.IOException ||
                e is System.UnauthorizedAccessException ||
                e is System.NotSupportedException ||
                e is System.IO.FileNotFoundException ||
                e is System.IO.InvalidDataException
            )
            {
                return Result.Error( e.Message );
            }
        }
    }
}