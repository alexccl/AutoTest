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
    public class RecordedMethodModelTest
    {
        [TestMethod]
        public void Recorded_Method_Init()
        {
            var x = InitTestModel();

            Assert.IsTrue(x.InstanceAtExecutionTime == "test");
            Assert.IsTrue(x.ExecutionComplete == false);
            Assert.IsTrue(x.ReturnTypeVal.Value == null);
            Assert.IsTrue(x.ReturnTypeVal.Type == typeof(double));
            Assert.IsTrue(x.TargetType == typeof(String));
        }

        [TestMethod]
        public void Recorded_Method_Ensure_Args_Maintain_Order()
        {
            var x = InitTestModel();
            Assert.IsTrue(x.Args[0].Type == typeof(String));
            Assert.IsTrue(x.Args[1].Type == typeof(int));
            Assert.IsTrue(x.Args[2].Type == typeof(DateTime));
        }

        [TestMethod]
        public void Recorded_Method_Test_Closeout()
        {
            var x = InitTestModel();
            x.CloseOutMethod(2.2);
            Assert.IsTrue((double)x.ReturnTypeVal.Value == 2.2);
        }

        [TestMethod]
        public void Recorded_Method_Test_Uniqueness_Of_Identifier()
        {
            var model1 = InitTestModel();
            var model2 = InitTestModel();

            Assert.IsFalse(model1.Identifier.Equals(model2.Identifier));
        }

        [TestMethod]
        public void Recorded_Method_Test_Equality_Override()
        {
            var x = InitTestModel();
            Assert.IsFalse(x.Equals(1));
            Assert.IsFalse(x.Equals(new DateTime()));
            Assert.IsTrue(x.Equals(x));


            var y = x;
            y.CloseOutMethod(new DateTime());
            Assert.IsTrue(x.Equals(y));

            Assert.IsFalse(x.Equals(InitTestModel()));
        }

        private RecordedMethod InitTestModel()
        {
            return new RecordedMethod(typeof(String), "test", new object[] { "arg1", 2, DateTime.Now }, DataHelper.MathPowerData.Entry.Method);
        }

    }
}
