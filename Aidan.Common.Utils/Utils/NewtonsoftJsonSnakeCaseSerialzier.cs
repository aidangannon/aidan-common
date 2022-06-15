using Aidan.Common.Core.Interfaces.Contract;
using Newtonsoft.Json;

namespace Aidan.Common.Utils.Utils
{
    public class NewtonsoftJsonSnakeCaseSerialzier : BaseJsonSerializer, IJsonSnakeCaseSerializer
    {
        public NewtonsoftJsonSnakeCaseSerialzier( IContractResolverAdapter contractResolver ) =>
            Settings.ContractResolver = contractResolver.Resolver;

        public string Serialize( object content ) => JsonConvert.SerializeObject( content, Settings );

        public T Deserialize<T>( string content ) => JsonConvert.DeserializeObject<T>( content, Settings );
    }
}