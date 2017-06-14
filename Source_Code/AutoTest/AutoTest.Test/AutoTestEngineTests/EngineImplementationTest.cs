using AutoTestEngine;
using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.InterceptionVerification.VerificationResult;
using AutoTestEngine.ProcessMultiplexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class EngineImplementationTest
    {
        [TestMethod]
        public void Engine_Implementation_Test_Verifiers_Added_To_Processing_Data()
        {
            var procData = TestClass.Method1Entry;
            var verifierResult = new List<VerificationFailure>()
            {
                new TypeSerializationFailure(typeof(double))
            };
            var pipelineResult = new VerificationPipelineResult();
            pipelineResult.AddFailures(verifierResult);

            var _verificationPipeline = new Mock<IVerificationPipeline>();
            var _processMultiplexer = new Mock<IProcessMultiplexer>();

            _verificationPipeline.Setup(x => x.VerifyInterception(It.IsAny<InterceptionProcessingData>()))
                                 .Returns(pipelineResult);

            bool hasVerifierResult = false;
            _processMultiplexer.Setup(x => x.Process(It.IsAny<InterceptionProcessingData>()))
                               .Callback((InterceptionProcessingData data) =>
                                         hasVerifierResult = data.VerificationFailures.Any())
                               .Returns(new ProcessResult());

            var SUT = new EngineImplementation(_verificationPipeline.Object, _processMultiplexer.Object);
            SUT.RunEngine(procData);

            Assert.IsTrue(hasVerifierResult);
        }
    }
}
