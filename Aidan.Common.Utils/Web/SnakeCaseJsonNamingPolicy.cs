using System.Text.Json;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Web
{
    public class SnakeCaseJsonNamingPolicy : JsonNamingPolicy
    {
        private readonly IJsonFieldNameParser _jsonFieldNameParser;

        public SnakeCaseJsonNamingPolicy( IJsonFieldNameParser jsonFieldNameParser ) =>
            _jsonFieldNameParser = jsonFieldNameParser;

        public override string ConvertName( string name ) =>
            _jsonFieldNameParser.Get( name );
    }
}