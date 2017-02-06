using AutoTestEngine.Helpers.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class SerializationResultTest
    {
        [TestMethod]
        public void Serialization_Result_Test_Test_Successful_Initialization()
        {
            var originalObject = "Test";
            var serializedObject = "Serialized Object";
            var SUT = SerializationResult.InitSuccessfulSerialization(originalObject, serializedObject);
            Assert.IsTrue(SUT.FailureException == null);
            Assert.IsTrue(SUT.Result == serializedObject);
            Assert.IsTrue(SUT.SerializedType.Equals(originalObject.GetType()));
            Assert.IsTrue(SUT.Success == true);
        }


        [TestMethod]
        public void Serialization_Result_Test_Test_Failure_Initialization()
        {
            var originalObject = "Test";
            var SUT = SerializationResult.InitFailedSerialization(originalObject, new Exception());
            Assert.IsTrue(SUT.FailureException != null);
            Assert.IsTrue(SUT.Result == null);
            Assert.IsTrue(SUT.SerializedType.Equals(originalObject.GetType()));
            Assert.IsTrue(SUT.Success == false);
        }
    }
}
