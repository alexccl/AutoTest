using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification.VerificationResult
{
    internal class TypeSerializationFailure : VerificationFailure
    {
        public Type FailedType { get; private set; }
        public TypeSerializationFailure(Type failedSerializationType) : base($"Failed to serialize type {failedSerializationType.ToString()}", Failures.SerializationError, false)
        {
            this.FailedType = failedSerializationType;
        }
    }
}
