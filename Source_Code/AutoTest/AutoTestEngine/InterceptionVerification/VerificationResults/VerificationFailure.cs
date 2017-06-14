using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification.VerificationResult
{
    internal abstract class VerificationFailure
    {
        public string FailureDescription{get; }
        public Guid FailureId { get; }
        public bool IsCriticalFailure { get; }

        public VerificationFailure(string description, Guid id, bool isCriticalFailure)
        {
            this.FailureDescription = description;
            this.FailureId = id;
            this.IsCriticalFailure = isCriticalFailure;
        }
    }
}
