using System;
using System.IO;
using System.Linq;
using System.Security;
using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class SolutionDirectoryGetter : ISolutionDirectoryGetter
    {
        private readonly IFileAdapter _fileAdapter;

        public SolutionDirectoryGetter( IFileAdapter fileAdapter ) { _fileAdapter = fileAdapter; }

        public ObjectResult<string> Get( )
        {
            var currentDirResult = _fileAdapter.GetCurrentDirectory( );
            if( currentDirResult.Status == OperationResultEnum.Failed )
            {
                return currentDirResult;
            }

            try
            {
                var directory = new DirectoryInfo( currentDirResult.Value );
                while( directory != null && !directory.GetFiles( "*.sln" ).Any( ) ) { directory = directory.Parent; }

                return new ObjectResult<string>
                {
                    Status = OperationResultEnum.Success,
                    Value = directory?.ToString( )
                };
            }
            catch( Exception e ) when ( e is ArgumentException ||
                                        e is ArgumentNullException ||
                                        e is DirectoryNotFoundException ||
                                        e is SecurityException ||
                                        e is PathTooLongException )
            {
                return new ObjectResult<string>
                {
                    Status = OperationResultEnum.Failed,
                    Msg = e.Message
                };
            }
        }
    }
}