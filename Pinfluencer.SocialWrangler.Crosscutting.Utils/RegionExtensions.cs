using System.Globalization;
using Aidan.Common.Core.Enum;

namespace Pinfluencer.SocialWrangler.Crosscutting.Utils
{
    public static class RegionExtensions
    {
        public static RegionInfo GetRegion( CountryEnum country ) { return new RegionInfo( country.ToString( ) ); }

        public static string Stringify( this CountryEnum country ) { return country.ToString( ); }
    }
}