using System;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal interface IRecordingMethodManager
    {
        event EventHandler<MethodRecordingCompleteEventArgs> MethodRecordingComplete;

        void ProcessCapture(InterceptionProcessingData processingData);
    }
}