using System;
using Aidan.Common.Core.Interfaces.Contract;
using Newtonsoft.Json.Serialization;

namespace Aidan.Common.Utils.Web
{
    public class JsonSnakeCaseResolver : DefaultContractResolver, IContractResolverAdapter
    {
        private readonly IJsonFieldNameParser _jsonFieldNameParser;

        public JsonSnakeCaseResolver( IJsonFieldNameParser jsonFieldNameParser )
        {
            _jsonFieldNameParser = jsonFieldNameParser;
        }
        
        public IContractResolver Resolver
        {
            get => this;
            set => throw new Exception( );
        }

        protected override string ResolvePropertyName( string propertyName ) =>
            _jsonFieldNameParser.Get( propertyName );
    }
}