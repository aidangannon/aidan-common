using System;
using Aidan.Common.Core.Enum;

namespace Aidan.Common.Core.Attributes
{
    [ AttributeUsage( AttributeTargets.Interface ) ]
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetimeEnum Scope { get; set; }
    }
}