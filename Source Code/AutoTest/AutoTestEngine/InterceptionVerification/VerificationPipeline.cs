using AutoTestEngine.InterceptionVerification.Verifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification
{
    internal class VerificationPipeline
    {
        private List<IVerifier> _verifiers;
        public VerificationPipeline(IVerifier[] verifiers)
        {
            _verifiers = verifiers.OrderBy(x => x.VerificationPriority).ToList();
        }

        public VerificationPipelineResult VerifyInterception(InterceptionProcessingData processingData)
        {
            foreach(var verifier in _verifiers)
            {
                var verificationResult = verifier.Verify(processingData);
                if (verificationResult.VerificationFailed)
                {
                    return new VerificationPipelineResult(true);
                }
            }

            return new VerificationPipelineResult(false);
        }
    }
}
