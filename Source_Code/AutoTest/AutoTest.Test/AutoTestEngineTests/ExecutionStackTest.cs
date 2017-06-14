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
    public class ExecutionStackTest
    {
        [TestMethod]
        public void Execution_Stack_Test_Defaults_To_Empty_Stack()
        {
            var SUT = new ExecutionStack();
            Assert.IsTrue(SUT.IsStackEmpty(1));
        }

        [TestMethod]
        public void Execution_Stack_Test_Sentinal_Value_For_Empty_Stack()
        {
            var SUT = new ExecutionStack();
            Assert.IsTrue(SUT.ExecutingGuid(0) == SUT.EmptyStackSentinel);
        }

        [TestMethod]
        public void Execution_Stack_Test_Clear_Stack()
        {
            var SUT = new ExecutionStack();
            SUT.ProcessEntry(1, Guid.NewGuid());
            SUT.ClearStack();
            Assert.IsTrue(SUT.IsStackEmpty(1));
        }

        [TestMethod]
        public void Execution_Stack_Test_Add_Guid_Makes_Stack_Nonempty()
        {
            var SUT = new ExecutionStack();
            SUT.ProcessEntry(1, Guid.NewGuid());
            Assert.IsTrue(!SUT.IsStackEmpty(1));
            SUT.ClearStack();
        }

        [TestMethod]
        public void Execution_Stack_Test_Exiting_Method_Removes_Guid()
        {
            var SUT = new ExecutionStack();
            SUT.ProcessEntry(1, Guid.NewGuid());
            SUT.ProcessExit(1);
            Assert.IsTrue(SUT.IsStackEmpty(1));
            SUT.ClearStack();
        }

        [TestMethod]
        public void Execution_Stack_Test_Method_Exception_Removes_Guid()
        {
            var SUT = new ExecutionStack();
            SUT.ProcessEntry(1, Guid.NewGuid());
            SUT.ProcessException(1);
            Assert.IsTrue(SUT.IsStackEmpty(1));
            SUT.ClearStack();
        }

        [TestMethod]
        public void Execution_Stack_Test_No_Access_To_Other_Thread()
        {
            var SUT = new ExecutionStack();
            SUT.ProcessEntry(1, Guid.NewGuid());
            Assert.IsTrue(SUT.IsStackEmpty(2));
            SUT.ClearStack();
        }

        [TestMethod]
        public void Execution_Stack_Test_Exit_On_Empty_Stack_Throws_Exception()
        {
            var SUT = new ExecutionStack();
            var exceptionThrown = false;

            try
            {
                SUT.ProcessExit(1);
            }
            catch(ExecutionStackEmptyException ex)
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            SUT.ClearStack();
        }

        [TestMethod]
        public void Execution_Stack_Test_Check_Equality_Of_Pushed_Method()
        {
            var SUT = new ExecutionStack();
            var newGuid = Guid.NewGuid();

            SUT.ProcessEntry(1, newGuid);
            Assert.IsTrue(SUT.ExecutingGuid(1) == newGuid);
        }

        [TestMethod]
        public void Execution_Stack_Test_Check_Equality_Of_Mult_Pushed_Methods()
        {
            var SUT = new ExecutionStack();
            var g1 = Guid.NewGuid();
            var g2 = Guid.NewGuid();

            SUT.ProcessEntry(1, g1);
            SUT.ProcessEntry(1, g2);
            Assert.IsTrue(SUT.ExecutingGuid(1) == g2);
            SUT.ProcessExit(1);
            Assert.IsTrue(SUT.ExecutingGuid(1) == g1);
        }

        [TestCleanup]
        public void Cleanup()
        {
            (new ExecutionStack()).ClearStack();
        }
    }
}
