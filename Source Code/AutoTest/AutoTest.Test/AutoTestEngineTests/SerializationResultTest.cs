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

            var SUT = SerializationResult.InitSuccessfulSerialization(originalObject, serializedObject, typeof(string));
            Assert.IsTrue(SUT.FailureException == null);
            Assert.IsTrue(SUT.SerializedValue.Value == serializedObject);
            Assert.IsTrue(SUT.SerializedValue.Type.Equals(originalObject.GetType()));
            Assert.IsTrue(SUT.Success == true);
        }


        [TestMethod]
        public void Serialization_Result_Test_Test_Failure_Initialization()
        {
            var originalObject = "Test";
            var SUT = SerializationResult.InitFailedSerialization(originalObject, new Exception(), typeof(string));
            Assert.IsTrue(SUT.FailureException != null);
            Assert.IsTrue(SUT.SerializedValue.Value == string.Empty);
            Assert.IsTrue(SUT.SerializedValue.Type.Equals(originalObject.GetType()));
            Assert.IsTrue(SUT.Success == false);
        }
    }
}
