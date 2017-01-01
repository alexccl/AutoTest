using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification.Verifiers
{
    internal class VerifierResult
    {
        public bool VerificationFailed { get; internal set; }

        public VerifierResult(bool hasFailed)
        {
            this.VerificationFailed = hasFailed;
        }
    }
}
