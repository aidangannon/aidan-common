using System;
using Newtonsoft.Json.Serialization;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface IContractResolverAdapter
    {
        IContractResolver Resolver { get; set; }
        JsonContract ResolveContract( Type type );
    }
}