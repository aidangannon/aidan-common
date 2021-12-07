using System;
using System.Threading;
using System.Threading.Tasks;
using Aidan.Common.Core;
using Aidan.Common.Core.Interfaces.Contract;
using Aidan.Common.Core.Interfaces.Excluded;

namespace Aidan.Common.Utils.EventDriven
{
    public abstract class BasePollingService : IPollingService
    {
        private readonly Action _work;
        private readonly int _interval;
        private readonly ITaskService _taskService;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly CancellationToken _token;

        protected BasePollingService( Action work, int interval, ITaskService taskService )
        {
            _work = work;
            _interval = interval;
            _taskService = taskService;
            _cancellationTokenSource = new CancellationTokenSource( );
            _token = _cancellationTokenSource.Token;
        }

        public void Cancel( ) => _cancellationTokenSource.Cancel( );
        public void DoWork( ) => _work( );

        public Result Initialize( )
        {
            _taskService.StartNew( Poll, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default );
            return Result.Success( );
        }

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