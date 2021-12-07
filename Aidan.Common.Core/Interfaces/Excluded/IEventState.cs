using System;
using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;

namespace Aidan.Common.Core.Interfaces.Excluded
{
    [ Service( Scope = ServiceLifetimeEnum.Singleton) ]
    [ ThreadSafe ]
    public interface IEventState<T>
    {
        public T Value { get; set; }
        public T PreviousValue { get; }
        event Action ValueChangedEvent;
    }
}