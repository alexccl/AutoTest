using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    internal class ThreadIdProvider : IThreadIdProvider
    {
        public int GetThreadId()
        {
            return Thread.CurrentThread.ManagedThreadId;
        }
    }
}
