using System.Linq;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.Utils
{
    public class JsonSnakeCaseFieldNameParser : IJsonFieldNameParser
    {
        public string Get( string name )
        {
            if( ! name.Any( char.IsLower ) ) return name;
            return string.Concat(
                    name
                        .Select( ( x, i ) => i > 0 && char.IsUpper( x ) ? "_" + x : x.ToString( ) ) )
                .ToLower( );
        }
    }
}