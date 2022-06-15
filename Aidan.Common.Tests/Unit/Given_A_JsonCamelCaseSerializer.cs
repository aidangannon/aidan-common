using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Utils.Test;
using Aidan.Common.Utils.Utils;

namespace Aidan.Common.Tests.Unit
{
    public abstract class Given_A_JsonCamelCaseSerializer : GivenWhenThen<IJsonCamelCaseSerializer>
    {
        protected override void Given( )
        {
            SUT = new NewtonsoftJsonCamelCaseSerializer( );
        }
    }
}