using System;
using System.Threading;
using System.Threading.Tasks;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BasePollingService : IPollingService
    {
        private readonly CancellationTokenSource _cancellationTokenSource;

        protected BasePollingService( Action work, int interval )
        {
            _cancellationTokenSource = new CancellationTokenSource( );
            var token = _cancellationTokenSource.Token;
            var listener = Task.Factory.StartNew( ( ) =>
            {
                while( true )
                {
                    Thread.Sleep( interval );
                    work( );
                    if( token.IsCancellationRequested )
                        break;
                }
                //TODO: parameterize cleanup
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default );
        }

        public void Cancel( ) => _cancellationTokenSource.Cancel( );
    }
}