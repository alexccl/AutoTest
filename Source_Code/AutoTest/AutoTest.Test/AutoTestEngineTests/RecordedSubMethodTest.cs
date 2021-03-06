﻿using AutoTestEngine.Helpers;
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
    public class RecordedSubMethodTest
    {
        [TestMethod]
        public void Recorded_Sub_Method_Identifier_Set()
        {
            var entry = TestClass.Method1Entry;
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid, entry.TargetType, entry.MethodArgs, entry.ReturnType, entry.Method);
            Assert.IsTrue(subMethod.Identifier.Equals(guid));
        }

        [TestMethod]
        public void Recorded_Sub_Method_Test_Constructor_Initialization()
        {
            var entry = TestClass.Method1Entry;
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid, entry.TargetType, entry.MethodArgs, entry.ReturnType, entry.Method);

            //target type
            Assert.IsTrue(subMethod.TargetType.Equals(entry.TargetType));

            //Method args
            for(int i = 0; i < entry.MethodArgs.Count; i++)
            {
                var typeVal = subMethod.Args[i];
                var arg = entry.MethodArgs[i];

                Assert.IsTrue(typeVal.GetType().Equals(arg.GetType()));
                //would test equality, but could be complex type without overloaded equality operator
            }

            //method name
            Assert.IsTrue(subMethod.MethodData.MethodName == entry.Method.Name);

            //execution complete
            Assert.IsTrue(subMethod.ExecutionComplete == false);

            //exception
            Assert.IsTrue(subMethod.MethodException == null);

            //return val
            Assert.IsTrue(subMethod.ReturnVal == null);
        }

        [TestMethod]
        public void Recorded_Sub_Method_Test_Method_Closeout_With_Return_Val()
        {
            var entry = TestClass.Method1Entry;
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid, entry.TargetType, entry.MethodArgs, entry.ReturnType, entry.Method);

            var returnVal = "Return Value";
            subMethod.CloseOutMethodWithReturnVal(returnVal);

            Assert.IsTrue(subMethod.ExecutionComplete == true);
            Assert.IsTrue(((string)subMethod.ReturnVal) == returnVal);
            Assert.IsTrue(subMethod.MethodException == null);
        }

        [TestMethod]
        public void Recorded_Sub_Method_Test_Method_Closeout_With_Exception()
        {
            var entry = TestClass.Method1Entry;
            var guid = Guid.NewGuid();
            var subMethod = new RecordedSubMethod(guid, entry.TargetType, entry.MethodArgs, entry.ReturnType, entry.Method);

            var returnVal = "Return Value";
            subMethod.CloseOutMethodWithException(new DivideByZeroException());

            Assert.IsTrue(subMethod.ExecutionComplete == true);
            Assert.IsTrue(subMethod.ReturnVal == null);
            Assert.IsTrue(subMethod.MethodException != null);
            Assert.IsTrue(subMethod.MethodException.GetType().Equals(typeof(DivideByZeroException)));
        }
    }
}
