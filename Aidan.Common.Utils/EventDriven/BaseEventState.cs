using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public class BaseEventState<T> : IEventState<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChangedEvent?.Invoke( );
            }
        }

        public event Action ValueChangedEvent;
    }
}