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

        public ObjectResult<string> GetDirectoryName( string filePath )
        {
            var result = Path.GetDirectoryName( filePath );
            if( string.IsNullOrEmpty( result ) )
            {
                return new ObjectResult<string>
                {
                    Msg = $"{filePath} not found",
                    Status = OperationResultEnum.Failed
                };
            }
            return new ObjectResult<string>
            {
                Value = result,
                Status = OperationResultEnum.Success
            };
        }
        
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

        public ObjectResult<string> ReadFile( string path )
        {
            try
            {
                var content = new StreamReader( path ).ReadToEnd( );
                return new ObjectResult<string> { Status = OperationResultEnum.Success, Value = content };
            }
            catch( Exception e )
            {
                return new ObjectResult<string> { Status = OperationResultEnum.Failed, Msg = e.Message };
            }
        }

        public Result WriteFile( string path, string content )
        {
            try
            {
                new StreamWriter( path ).Write( content );
                return Result.Success( );
            }
            catch( Exception e )
            {
                return Result.Error( e.Message );
            }
        }
    }
}