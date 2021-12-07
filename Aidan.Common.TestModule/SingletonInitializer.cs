using Aidan.Common.Core;
using Aidan.Common.TestModule.Core.Interfaces.Contract;

namespace Aidan.Common.TestModule
{
    public class SingletonInitializer : ISingletonInitializer
    {
        public Result Initialize( )
        {
            return Result.Success(  );
        }
    }
}