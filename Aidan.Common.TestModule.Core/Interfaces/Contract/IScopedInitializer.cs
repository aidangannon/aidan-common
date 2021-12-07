using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.TestModule.Core.Interfaces.Contract
{
    [ Service( Scope = ServiceLifetimeEnum.Scoped ) ]
    public interface IScopedInitializer : IInitialisable
    {
    }
}