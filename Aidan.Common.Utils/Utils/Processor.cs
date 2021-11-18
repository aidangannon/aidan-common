using System;
using System.ComponentModel;
using System.Diagnostics;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class Processor : IProcessor
    {
        public Result RunAndWait( string process, string args )
        {
            var processObj = new Process();
            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = process,
                Arguments = args
            };
            processObj.StartInfo = startInfo;
            try
            {
                processObj.Start( );
                processObj.WaitForExit( );
            }
            catch ( Exception ex ) when (
                ex is InvalidOperationException
                || ex is Win32Exception
                || ex is PlatformNotSupportedException
                )
            {
                return Result.Error( ex.Message );
            }

            if( processObj.ExitCode == 1 )
            {
                return Result.Success( );
            }
            else
            {
                return Result.Error( "there was an error in running execution :P" );
            }
        }
    }
}