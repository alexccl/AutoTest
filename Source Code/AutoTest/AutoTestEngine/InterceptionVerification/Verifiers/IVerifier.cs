using AutoTestEngine.InterceptionVerification.VerificationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification.Verifiers
{
    internal interface IVerifier
    {
        /// <summary>
        /// Determines where this verifier is ordered in the pipeline, the lower the number the sooner it will be executed
        /// </summary>
        int VerificationPriority { get; }
        List<VerificationFailure> Verify(InterceptionProcessingData processingData);
    }
}
