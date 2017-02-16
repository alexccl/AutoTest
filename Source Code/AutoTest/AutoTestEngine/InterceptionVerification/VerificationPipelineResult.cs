using AutoTestEngine.InterceptionVerification.VerificationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification
{
    /// <summary>
    /// Compiles all the verifier results from each verification step in the pipeline
    /// </summary>
    internal class VerificationPipelineResult
    {
        public List<VerificationFailure> Failures { get; private set; }

        public VerificationPipelineResult()
        {
            this.Failures = new List<VerificationFailure>();
        }

        public bool HasCriticalFailure
        {
            get
            {
                return this.Failures.Any(x => x.IsCriticalFailure);
            }
        }

        public bool HasAnyFailure
        {
            get
            {
                return Failures.Any();
            }
        }

        public void AddFailure(VerificationFailure failure)
        {
            Failures.Add(failure);
        }

        public void AddFailures(List<VerificationFailure> failures)
        {
            Failures.AddRange(failures);
        }

        public bool HasFailure(Guid Id)
        {
            return Failures.Any(x => x.FailureId.Equals(Id));
        }

    }
}
