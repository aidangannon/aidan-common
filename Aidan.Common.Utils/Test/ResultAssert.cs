using Aidan.Common.Core;
using Aidan.Common.Core.Enum;
using NUnit.Framework;

namespace Aidan.Common.Utils.Test
{
    public static class ResultAssert
    {
        public static void IsSuccess<T>( ObjectResult<T> result )
        {
            Assert.AreEqual( OperationResultEnum.Success, result.Status );
        }
        
        public static void IsWarning<T>( ObjectResult<T> result )
        {
            Assert.AreEqual( OperationResultEnum.Warning, result.Status );
        }
        
        public static void IsFailiure<T>( ObjectResult<T> result )
        {
            Assert.AreEqual( OperationResultEnum.Failed, result.Status );
        }
    }
}