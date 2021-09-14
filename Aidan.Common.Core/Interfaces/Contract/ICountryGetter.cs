using System.Collections.ObjectModel;
using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;

namespace Aidan.Common.Core.Interfaces.Contract
{
    [ Service( Scope = ServiceLifetimeEnum.Singleton ) ]
    public interface ICountryGetter
    {
        public ReadOnlyDictionary<CountryEnum, string> Countries { get; }
    }
}