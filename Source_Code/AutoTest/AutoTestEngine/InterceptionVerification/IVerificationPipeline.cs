namespace AutoTestEngine.InterceptionVerification
{
    internal interface IVerificationPipeline
    {
        VerificationPipelineResult VerifyInterception(InterceptionProcessingData processingData);
    }
}