using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.ProcessMultiplexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    internal class EngineImplementation : IEngineImplementation
    {
        private IVerificationPipeline _verificationPipeline;
        private IProcessMultiplexer _processMultiplexor;

        public EngineImplementation(IVerificationPipeline verificationPipeline, IProcessMultiplexer processMultiplexer)
        {
            _verificationPipeline = verificationPipeline;
            _processMultiplexor = processMultiplexer;
        }

        public ProcessResult RunEngine(InterceptionProcessingData processingData)
        {
            var verifierResult = _verificationPipeline.VerifyInterception(processingData);
            processingData.AddVerificationFailures(verifierResult.Failures);

            var res = _processMultiplexor.Process(processingData);
            var x = DAL.TexFileImplementation.Repository.StoredObject;
            return res;
        }
    }
}
