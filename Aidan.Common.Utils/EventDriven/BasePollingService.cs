using System;
using System.Threading;
using System.Threading.Tasks;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BasePollingService : IPollingService
    {
        private readonly Action _work;
        private readonly int _interval;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _token;

        protected BasePollingService( Action work, int interval )
        {
            _work = work;
            _interval = interval;
            _cancellationTokenSource = new CancellationTokenSource( );
            _token = _cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew( Poll, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default );
        }

        public void Cancel( ) => _cancellationTokenSource.Cancel( );
        public Result Initialize( ) => Result.Success( );

        private void Poll( )
        {
            while( true )
            {
                Thread.Sleep( _interval );
                _work( );
                if( _token.IsCancellationRequested )
                    break;
            }
        }
    }
}