namespace AutoTestEngine.ProcessMultiplexer
{
    internal interface IProcessMultiplexer
    {
        ProcessResult Process(InterceptionProcessingData processingData);
    }
}