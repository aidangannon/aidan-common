using Aidan.Common.Core.Interfaces.Contract;
using Newtonsoft.Json;

namespace Aidan.Common.Utils.Utils
{
    public class JsonCamelCaseSerializer : BaseJsonSerializer, IJsonCamelCaseSerializer
    {
        public string Serialize( object content ) { return JsonConvert.SerializeObject( content, Settings ); }

        public T Deserialize<T>( string content ) { return JsonConvert.DeserializeObject<T>( content, Settings ); }
    }
}