using System;

namespace Aidan.Common.Core.Attributes
{
    public class CountryAttribute : Attribute
    {
        public CountryAttribute( string name ) { Name = name; }

        public string Name { get; }
    }
}