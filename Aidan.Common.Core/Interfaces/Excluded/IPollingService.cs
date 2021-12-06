using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;

namespace Aidan.Common.Core.Interfaces.Excluded
{
    [ Service( Scope = ServiceLifetimeEnum.Singleton ) ]
    public interface IPollingService : IInitialisable
    {
        void Cancel( );
        void DoWork( );
    }
}