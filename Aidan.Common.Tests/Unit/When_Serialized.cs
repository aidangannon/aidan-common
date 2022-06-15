using NUnit.Framework;

namespace Aidan.Common.Tests.Unit
{
    public class When_Serialized : Given_A_JsonCamelCaseSerializer
    {
        [ Test ]
        public void Then_Resulting_Json_String_Is_In_Camel_Case_Format( )
        {
            var @object = new
            {
                Value = 1,
                ValueTwo = "string value"
            };
            Assert.AreEqual("{\"value\":1,\"valueTwo\":\"string value\"}", SUT.Serialize( @object ));
        }
    }
}