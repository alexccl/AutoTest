using AutoTestEngine.InterceptionVerification.Verifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification
{
    /// <summary>
    /// Pipeline that each interception must go through
    /// </summary>
    internal class VerificationPipeline
    {
        private List<IVerifier> _verifiers;
        public VerificationPipeline(IVerifier[] verifiers)
        {
            _verifiers = verifiers.OrderBy(x => x.VerificationPriority).ToList();
        }

        /// <summary>
        /// Processes each interception in a pipeline
        /// </summary>
        /// <param name="processingData">the data of this interception</param>
        /// <returns>Pipeline result</returns>
        public VerificationPipelineResult VerifyInterception(InterceptionProcessingData processingData)
        {
            var result = new VerificationPipelineResult();
            foreach(var verifier in _verifiers)
            {
                var verificationResult = verifier.Verify(processingData);
                result.AddFailures(verificationResult);

                if (result.HasCriticalFailure) return result;
            }

            return result;
        }
    }
}
