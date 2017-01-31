using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    internal interface IExecutionStack
    {
        Guid EmptyStackSentinel { get; }
        bool IsStackEmpty(int threadId);
        Guid ExecutingGuid(int threadId);
        void ProcessEntry(int threadId, Guid newMethodGuid);
        void ProcessExit(int threadId);
        void ProcessException(int threadId);
        void ClearStack();
    }
}
