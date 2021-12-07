using System;
using System.Threading;
using System.Threading.Tasks;

namespace Aidan.Common.Core.Interfaces.Contract
{
    public interface ITaskService
    {
        void StartNew( Action action,
            CancellationToken cancellationToken,
            TaskCreationOptions creationOptions,
            TaskScheduler scheduler );

        void Delay( int msDelay );
    }
}