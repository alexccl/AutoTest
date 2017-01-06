using AutoTestEngine;
using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.InterceptionVerification.Verifiers;
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
    public class VerificationPipelineTests
    {
        private VerifierResult successfulVerify = new VerifierResult(false);
        private VerifierResult failureVerify = new VerifierResult(true);
        [TestMethod]
        public void VerificationPipelineSingleFailureTest()
        {
            var mock = new Mock<IVerifier>();
            mock.Setup(x => x.VerificationPriority).Returns(1);
            mock.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(failureVerify);

            var verifiers = new IVerifier[] { mock.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(null);
            Assert.IsTrue(res.VerificationFailed);
        }

        [TestMethod]
        public void VerificationPipelineSingleSuccessTest()
        {
            var mock = new Mock<IVerifier>();
            mock.Setup(x => x.VerificationPriority).Returns(1);
            mock.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var verifiers = new IVerifier[] { mock.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(null);
            Assert.IsFalse(res.VerificationFailed);
        }

        [TestMethod]
        public void VerificationPipelineMultFailureTest()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(2);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(failureVerify);

            var verifiers = new IVerifier[] { mock2.Object, mock1.Object};
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(null);
            Assert.IsTrue(res.VerificationFailed);
        }

        [TestMethod]
        public void VerificationPipelineMultSuccessTest()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(2);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var verifiers = new IVerifier[] { mock2.Object, mock1.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(null);
            Assert.IsFalse(res.VerificationFailed);
        }

        [TestMethod]
        public void VerificationPipelineFailureBreaksVerificationChain()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(2);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(failureVerify);

            var mock3 = new Mock<IVerifier>();
            mock3.Setup(x => x.VerificationPriority).Returns(3);
            mock3.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var verifiers = new IVerifier[] { mock2.Object, mock1.Object, mock3.Object};
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(null);
            mock3.Verify(x => x.Verify(It.IsAny<InterceptionProcessingData>()), Times.Never);
        }

        [TestMethod]
        public void VerificationPipelineVerifierOrdering()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(2);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(successfulVerify);

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(1);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(failureVerify);

            var verifiers = new IVerifier[] { mock1.Object, mock2.Object};
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(null);
            mock1.Verify(x => x.Verify(It.IsAny<InterceptionProcessingData>()), Times.Never);
        }
    }
}
