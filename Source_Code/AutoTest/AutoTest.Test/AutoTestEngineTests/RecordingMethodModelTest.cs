using AutoTestEngine;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class RecordingMethodModelTest
    {
        [TestMethod]
        public void Recordingd_Method_Init()
        {
            var x = InitTestModel();

            Assert.IsTrue(x.InstanceAtExecutionTime.Value == "test");
            Assert.IsTrue(x.IsExecutionComplete == false);
            Assert.IsTrue(x.ReturnTypeVal == null);
            Assert.IsTrue(x.TargetType == typeof(String));
            Assert.IsTrue(x.SubMethods != null);
            Assert.IsTrue(x.SubMethods.Count == 0);
        }

        [TestMethod]
        public void Recording_Method_Ensure_Args_Maintain_Order()
        {
            var x = InitTestModel();
            Assert.IsTrue(x.Args[0].GetType() == typeof(String));
            Assert.IsTrue(x.Args[1].GetType() == typeof(int));
            Assert.IsTrue(x.Args[2].GetType() == typeof(DateTime));
        }

        [TestMethod]
        public void Recording_Method_Test_Closeout()
        {
            var x = InitTestModel();
            x.CloseOutMethodWithReturnVal(2.2);
            Assert.IsTrue((double)x.ReturnTypeVal == 2.2);
            Assert.IsTrue(x.IsExecutionComplete == true);
        }

        [TestMethod]
        public void Recorded_Method_Test_Exception_Closeout()
        {
            var x = InitTestModel();
            x.CloseOutMethodWithException(new InvalidTimeZoneException());
            Assert.IsTrue(x.MethodException != null);
            Assert.IsTrue(x.ReturnTypeVal == null);
            Assert.IsTrue(typeof(InvalidTimeZoneException).Equals(x.MethodException.GetType()));
            Assert.IsTrue(x.IsExecutionComplete == true);
        }

        [TestMethod]
        public void Recording_Method_Test_Uniqueness_Of_Identifier()
        {
            var model1 = InitTestModel();
            var model2 = InitTestModel();

            Assert.IsFalse(model1.Identifier.Equals(model2.Identifier));
        }

        [TestMethod]
        public void Recording_Method_Test_Equality_Override()
        {
            var x = InitTestModel();
            Assert.IsFalse(x.Equals(1)); //different type should not be equal
            Assert.IsFalse(x.Equals(new DateTime())); //different type should not be equal
            Assert.IsTrue(x.Equals(x)); 


            var y = x;
            y.CloseOutMethodWithReturnVal(2.2);
            Assert.IsTrue(x.Equals(y));

            Assert.IsFalse(x.Equals(InitTestModel()));
        }

        private RecordingMethod InitTestModel()
        {
            var instance = new SerializedValue(typeof(string), "test");
            var args = new List<object>()
            {
                "arg1",
                2,
                DateTime.Now
            };

            return new RecordingMethod(Guid.NewGuid(), instance, args, DataHelper.MathPowerData.Entry.Method);
        }

    }
}
