using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BaseEventState<T> : IEventState<T>
    {
        private readonly object _ness;
        private T _value;

        protected BaseEventState( )
        {
            _ness = new object( );
        }

        public T PreviousValue { get; private set; }

        public T Value
        {
            get => _value;
            set
            {
                lock( _ness )
                {
                    PreviousValue = _value;
                    _value = value;
                    ValueChangedEvent?.Invoke( );
                }
            }
        }

        public event Action ValueChangedEvent;
    }
}