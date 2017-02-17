using AutoTestEngine;
using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.InterceptionVerification.VerificationResult;
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
        private VerificationPipelineResult _successfulPipeRes;
        private VerificationPipelineResult _failurePipeRes;
        private List<VerificationFailure> _failureVerification;
        private List<VerificationFailure> _successfulVerification;

        [TestInitialize]
        public void Setup()
        {
            _successfulPipeRes = new VerificationPipelineResult();
            _failurePipeRes = new VerificationPipelineResult();
            _failurePipeRes.AddFailure(new TypeSerializationFailure(typeof(double)));

            _failureVerification = new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) };
            _successfulVerification = new List<VerificationFailure>();
        }

        [TestMethod]
        public void VerificationPipelineSingleFailureTest()
        {
            var mock = new Mock<IVerifier>();
            mock.Setup(x => x.VerificationPriority).Returns(1);
            mock.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_failureVerification);

            var verifiers = new IVerifier[] { mock.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(TestClass.Method1Entry);
            Assert.IsTrue(res.HasAnyFailure);
        }

        [TestMethod]
        public void VerificationPipelineSingleSuccessTest()
        {
            var mock = new Mock<IVerifier>();
            mock.Setup(x => x.VerificationPriority).Returns(1);
            mock.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_successfulVerification);

            var verifiers = new IVerifier[] { mock.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(TestClass.Method1Entry);
            Assert.IsFalse(res.HasAnyFailure);
        }

        [TestMethod]
        public void VerificationPipelineMultFailureTest()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_successfulVerification);

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(2);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_failureVerification);

            var verifiers = new IVerifier[] { mock2.Object, mock1.Object};
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(TestClass.Method1Entry);
            Assert.IsTrue(res.HasAnyFailure);
        }

        [TestMethod]
        public void VerificationPipelineMultSuccessTest()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_successfulVerification);

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(2);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_successfulVerification);

            var verifiers = new IVerifier[] { mock2.Object, mock1.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(TestClass.Method1Entry);
            Assert.IsFalse(res.HasAnyFailure);
        }

        [TestMethod]
        public void VerificationPipelineMult_Critical_Failure_Test()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(new List<VerificationFailure>() { new CriticalFailure() });

            var mock2 = new Mock<IVerifier>();
            mock2.Setup(x => x.VerificationPriority).Returns(2);
            mock2.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(_successfulVerification);

            var verifiers = new IVerifier[] { mock2.Object, mock1.Object };
            var pipeline = new VerificationPipeline(verifiers);

            var res = pipeline.VerifyInterception(TestClass.Method1Entry);
            Assert.IsTrue(res.HasAnyFailure);
            Assert.IsTrue(res.HasCriticalFailure);
            mock2.Verify(x => x.Verify(It.IsAny<InterceptionProcessingData>()), Times.Never);
        }

        [TestMethod]
        public void VerificationPipelineMult_Failures_Added_To_Proc_Data()
        {
            var mock1 = new Mock<IVerifier>();
            mock1.Setup(x => x.VerificationPriority).Returns(1);
            mock1.Setup(x => x.Verify(It.IsAny<InterceptionProcessingData>())).Returns(new List<VerificationFailure>() { new TypeSerializationFailure(typeof(double)) });

            var verifiers = new IVerifier[] { mock1.Object};
            var pipeline = new VerificationPipeline(verifiers);

            var interception = TestClass.Method1Entry;
            var res = pipeline.VerifyInterception(interception);
            Assert.IsTrue(interception.VerificationFailures.Any());
        }

        private class CriticalFailure : VerificationFailure
        {
            public CriticalFailure() : base(String.Empty, Guid.NewGuid(), true)
            {
            }
        }
    }
}
