using System;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BaseEventCondition : IEventCondition
    {
        private readonly Func<bool> _predicate;
        private readonly object _ness;

        protected BaseEventCondition( Func<bool> predicate )
        {
            _predicate = predicate;
            _ness = new object( );
        }
        
        public event Action Valid;
        public event Action Invalid;

        public bool Evaluate( )
        {
            lock( _ness )
            {
                var valid = _predicate( );
                if( valid )
                {
                    Valid?.Invoke( );
                }
                else
                {
                    Invalid?.Invoke( );
                }
                return valid;
            }
        }
    }
}