using Aidan.Common.Core.Interfaces.Contract;
using Newtonsoft.Json;

namespace Aidan.Common.Utils.Utils
{
    public class NewtonsoftJsonPascalCaseSerializer : BaseJsonSerializer, IJsonPascalCaseSerializer
    {
        public string Serialize( object content ) { return JsonConvert.SerializeObject( content ); }

        public T Deserialize<T>( string content ) { return JsonConvert.DeserializeObject<T>( content, Settings ); }
    }
}