using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Aidan.Common.Core.Enum
{
    //TODO => MAKE DYNAMIC ( REFLECTION )
    [ JsonConverter( typeof( StringEnumConverter ) ) ]
    public enum GenderEnum
    {
        Unknown = 0,
        Male = 1,
        Female = 2
    }
}