using AutoTestEngine.ProcessMultiplexer;

namespace AutoTestEngine
{
    internal interface IEngineImplementation
    {
        ProcessResult RunEngine(InterceptionProcessingData processingData);
    }
}