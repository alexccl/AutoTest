using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.InterceptionVerification.VerificationResult;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class VerificationPipelineResultTest
    {
        private class CriticalFailure : VerificationFailure
        {
            public CriticalFailure() : base(String.Empty, Guid.NewGuid(), true)
            {
            }
        }

        [TestMethod]
        public void Verification_PipeLine_Result_Success_Test()
        {
            var SUT = new VerificationPipelineResult();
            Assert.IsTrue(SUT.Failures.Any() == false);
            Assert.IsTrue(SUT.HasAnyFailure == false);
            Assert.IsTrue(SUT.HasCriticalFailure == false);
        }

        [TestMethod]
        public void Verification_PipeLine_Result_Add_Failure_Results_In_Failure()
        {
            var SUT = new VerificationPipelineResult();
            Assert.IsTrue(SUT.HasAnyFailure == false);
            SUT.AddFailure(new TypeSerializationFailure(typeof(double)));
            Assert.IsTrue(SUT.HasAnyFailure == true);
        }

        [TestMethod]
        public void Verification_PipeLine_Result_Add_Critical_Failure_Results_In_Critical_Failure()
        {
            var SUT = new VerificationPipelineResult();
            Assert.IsTrue(SUT.HasAnyFailure == false);
            SUT.AddFailure(new CriticalFailure());
            Assert.IsTrue(SUT.HasCriticalFailure == true);

        }
    }
}
