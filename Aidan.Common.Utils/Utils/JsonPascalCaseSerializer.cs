using Aidan.Common.Core.Interfaces.Contract;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Aidan.Common.Utils.Utils
{
    public class JsonPascalCaseSerializer : BaseJsonSerializer, IJsonCamelAndPascalCaseSerializer
    {
        public string Serialize( object content )
        {
            return JsonConvert.SerializeObject( content, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver( )
            } );
        }

        public T Deserialize<T>( string content ) { return JsonConvert.DeserializeObject<T>( content, Settings ); }
    }
}