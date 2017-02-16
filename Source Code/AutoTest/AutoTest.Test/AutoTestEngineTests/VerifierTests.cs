using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using AutoTestEngine.DAL;
using AutoTestEngine.DAL.Helpers;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.InterceptionVerification.Verifiers;
using AutoTestEngine;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class VerifierTests
    {
        private EngineConfiguration _config = new EngineConfiguration();
        private Mock<IUnserializableTypeHelper> _successUnserializableTypehelper;
        private Mock<IUnserializableTypeHelper> _failUnserializableTypehelper;
        private Mock<ISerializationHelper> _successSerHelper;
        private Mock<ISerializationHelper> _failSerHelper;
        
        [TestInitialize]
        public void Setup()
        { 
            _successUnserializableTypehelper = new Mock<IUnserializableTypeHelper>();
            _successUnserializableTypehelper.Setup(x => x.IsUnserializable(It.IsAny<Type>())).Returns(false);

            _failUnserializableTypehelper = new Mock<IUnserializableTypeHelper>();
            _failUnserializableTypehelper.Setup(x => x.IsUnserializable(It.IsAny<Type>())).Returns(true);

            _successSerHelper = new Mock<ISerializationHelper>();
            _successSerHelper.Setup(x => x.Serialize(It.IsAny<object>())).Returns(SerializationResult.InitSuccessfulSerialization(new object(), String.Empty));

            _failSerHelper = new Mock<ISerializationHelper>();
            _failSerHelper.Setup(x => x.Serialize(It.IsAny<object>())).Returns(SerializationResult.InitFailedSerialization(new object(), new StackOverflowException()));
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Exit_Data_No_Serialization_Errors()
        {
            var SUT = new InputParameterVerifier(_successSerHelper.Object, _successUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Exit, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(!res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Exit_Data_Serialization_Errors()
        {
            var SUT = new InputParameterVerifier(_successSerHelper.Object, _failUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Exit, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(!res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Exc_Data_No_Serialization_Errors()
        {
            var SUT = new InputParameterVerifier(_successSerHelper.Object, _successUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Exception, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(!res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Exc_Data_Serialization_Errors()
        {
            var SUT = new InputParameterVerifier(_successSerHelper.Object, _failUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Exception, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(!res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Ent_Data_No_Serialization_Errors()
        {
            var SUT = new InputParameterVerifier(_successSerHelper.Object, _successUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Exception, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(!res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Ent_Data_Serialization_Errors()
        {
            var SUT = new InputParameterVerifier(_successSerHelper.Object, _failUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Entry, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Failed_Serialization_Fails_Verifier()
        {
            var SUT = new InputParameterVerifier(_failSerHelper.Object, _successUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Entry, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(res.Any());
        }

        [TestMethod]
        public void Input_Parameter_Verifier_Test_Failed_Serialization_Adds_To_DB()
        {
            var SUT = new InputParameterVerifier(_failSerHelper.Object, _successUnserializableTypehelper.Object);
            var data = new InterceptionProcessingData(DataHelper.DateTimeAddDaysData.Entry, _config);
            var res = SUT.Verify(data);

            Assert.IsTrue(res.Any());
            _successUnserializableTypehelper.Verify(x => x.AddUnserializableType(It.IsAny<Type>()), Times.Once);
        }


    }
}
