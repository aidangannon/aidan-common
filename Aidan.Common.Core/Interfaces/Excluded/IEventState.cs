using System;
using Aidan.Common.Core.Attributes;
using Aidan.Common.Core.Enum;

namespace Aidan.Common.Core.Interfaces.Excluded
{
    [ Service( Scope = ServiceLifetimeEnum.Singleton) ]
    public interface IEventState<T>
    {
        public T Value { get; set; }
        event Action ValueChangedEvent;
    }
}