using Aidan.Common.Core;
using Aidan.Common.TestModule.Core.Interfaces.Contract;

namespace Aidan.Common.TestModule
{
    public class ScopedInitializer : IScopedInitializer
    {
        public Result Initialize( )
        {
            return Result.Success( );
        }
    }
}