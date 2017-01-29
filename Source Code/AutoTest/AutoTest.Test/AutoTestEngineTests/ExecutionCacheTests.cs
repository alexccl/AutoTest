using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class ExecutionCacheTests
    {
        [TestMethod]
        public void Execution_Cache_Test_Get_Returns_Empty_List_When_Not_Exists()
        {
            var executionCache = new ExecutionCache();
            var methods = executionCache.GetMethods(1);

            Assert.IsTrue(methods != null);
            Assert.IsTrue(methods.Count == 0);
            executionCache.ClearCache();
        }

        [TestMethod]
        public void Execution_Cache_Test_Clear_Cache_Empties_Methods()
        {
            var entry = TestClass.Method1Entry;
            var method = new RecordedMethod(entry.TargetType, "", null, entry.Method);
            var executionCache = new ExecutionCache();
            executionCache.GetMethods(1).Add(method);

            var methods = executionCache.GetMethods(1);
            Assert.IsTrue(methods.Count == 1);

            executionCache.ClearCache();
            methods = executionCache.GetMethods(1);
            Assert.IsTrue(methods.Count == 0);
        }

        [TestMethod]
        public void Execution_Cache_Test_Get_Returns_Val_When_Exists()
        {
            var entry = TestClass.Method1Entry;
            var method = new RecordedMethod(entry.TargetType, "", null, entry.Method);
            var executionCache = new ExecutionCache();
            executionCache.GetMethods(1).Add(method);

            var method2 = executionCache.GetMethods(1);
            Assert.IsTrue(method2.Count > 0);
            executionCache.ClearCache();
        }

        [TestMethod]
        public void Execution_Cache_Test_Dif_Instance_Returns_Same_Value()
        {
            var entry = TestClass.Method1Entry;
            var method = new RecordedMethod(entry.TargetType, "", null, entry.Method);
            var executionCache = new ExecutionCache();
            executionCache.GetMethods(1).Add(method);

            var executionCache2 = new ExecutionCache();
            var method2 = executionCache.GetMethods(1);
            Assert.IsTrue(method2.Count > 0);
            executionCache.ClearCache();
        }

        [TestMethod]
        public void Execution_Cache_Test_No_Access_To_Diff_Thread()
        {
            var entry = TestClass.Method1Entry;
            var method = new RecordedMethod(entry.TargetType, "", null, entry.Method);
            var executionCache = new ExecutionCache();
            executionCache.GetMethods(1).Add(method);

            var thread2Methods = executionCache.GetMethods(2);
            Assert.IsTrue(thread2Methods.Count == 0);
            executionCache.ClearCache();
        }

        [TestMethod]
        public void Execution_Cache_Test_Modify_Method_Saves()
        {
            var entry = TestClass.Method1Entry;
            var method = new RecordedMethod(entry.TargetType, "", null, entry.Method);
            method.CloseOutMethod("blah");
            var executionCache = new ExecutionCache();
            executionCache.GetMethods(1).Add(method);

            var method2 = executionCache.GetMethods(1);
            Assert.IsTrue(method2.FirstOrDefault().ExeuctionComplete);
            executionCache.ClearCache();
        }
    }
}
