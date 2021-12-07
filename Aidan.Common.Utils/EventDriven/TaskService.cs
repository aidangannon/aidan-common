using System;
using System.Threading;
using System.Threading.Tasks;
using Aidan.Common.Core.Interfaces.Contract;

namespace Aidan.Common.Utils.EventDriven
{
    public class TaskService : ITaskService
    {
        public void StartNew( Action action,
            CancellationToken cancellationToken,
            TaskCreationOptions creationOptions,
            TaskScheduler scheduler ) => Task.Factory.StartNew( action, cancellationToken, creationOptions, scheduler );

        public void Delay( int msDelay ) => Task.Delay( msDelay );
    }
}