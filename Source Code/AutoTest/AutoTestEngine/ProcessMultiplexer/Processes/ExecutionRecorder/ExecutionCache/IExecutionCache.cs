using System.Collections.Generic;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    internal interface IExecutionCache
    {
        List<RecordedMethod> GetMethods(int threadId);

        void ClearCache();
    }
}