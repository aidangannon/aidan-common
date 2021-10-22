using System;
using System.IO;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class WindowsFileAdapter : IFileAdapter
    {
        public Result Exists( string path ) =>
            File.Exists( path ) ? Result.Success( ) : Result.Error( $"file at {path} was not found" ) ;

        public ObjectResult<string> GetFileExtension( string filePath )
        {
            try
            {
                return new ObjectResult<string>
                    { Status = OperationResultEnum.Success, Value = Path.GetExtension( filePath ) };
            }
            catch( ArgumentException e )
            {
                return new ObjectResult<string> { Msg = e.Message, Status = OperationResultEnum.Failed };
            }
        }
    }
}