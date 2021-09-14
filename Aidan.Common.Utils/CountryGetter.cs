using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils
{
    public class CountryGetter : ICountryGetter
    {
        public CountryGetter( )
        {
            var countries = new Dictionary<CountryEnum, string>( );
            foreach( var valueTuple in( Enum.GetValues( typeof( CountryEnum ) ) as CountryEnum [ ] ??
                                        throw new InvalidOperationException( ) )
                .Select( x => ( x, typeof( CountryEnum ).GetMember( x.ToString( ) ).FirstOrDefault( ) ) )
                .Select( x => ( x, x.Item2.GetCustomAttribute<CountryAttribute>( )?.Name ) ) )
                countries.Add( valueTuple.x.x, valueTuple.Name );
            Countries = new ReadOnlyDictionary<CountryEnum, string>( countries );
        }

        public ReadOnlyDictionary<CountryEnum, string> Countries { get; }
    }
}