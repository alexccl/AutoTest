using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification
{
    class VerificationPipelineResult
    {
        public bool VerificationFailed { get; private set; }
        public VerificationPipelineResult(bool verificationFailed)
        {
            this.VerificationFailed = verificationFailed;
        }
    }
}
