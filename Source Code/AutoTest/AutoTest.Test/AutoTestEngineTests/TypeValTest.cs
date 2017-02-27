using AutoTestEngine;
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
    public class TypeValTest
    {
        [TestMethod]
        public void Type_Val_Test_Test_Getters_And_Setters()
        {
            //I don't usually like testing getters and setters, but since there is some logic in them it's applicable
            var typeVal = new TypeValModel(typeof(string), "Test");
            Assert.IsTrue(typeVal.Type.Equals(typeof(string)));
            Assert.IsTrue(((string)typeVal.Value) == "Test");


            typeVal = new TypeValModel() { Type = typeof(string), Value = "Test" };
            Assert.IsTrue(typeVal.Type.Equals(typeof(string)));
            Assert.IsTrue(((string)typeVal.Value) == "Test");
        }

        [TestMethod]
        public void Type_Val_Test_Setting_Null_Wont_Throw_Exception()
        {
            var exceptionThrown = false;
            try
            {
                var typeVal = new TypeValModel(typeof(string), null);


                typeVal = new TypeValModel() { Type = null, Value = "Test" };
            }
            catch(AutoTestEngineException ex)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown == false);
        }

        [TestMethod]
        public void Type_Val_Test_Assign_Val_Of_Same_Type_Wont_Throw_Excpetion()
        {
            var exceptionThrown = false;
            try
            {
                var typeVal = new TypeValModel()
                {
                    Type = typeof(string),
                    Value = "Test"
                };
            }
            catch (AutoTestEngineException)
            {
                exceptionThrown = true;
            }

            Assert.IsTrue(exceptionThrown == false);
        }
    }
}
