using System.Collections.Generic;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache
{
    internal interface IExecutionCache
    {
        List<RecordingMethod> GetMethods(int threadId);

        void ClearCache();
    }
}