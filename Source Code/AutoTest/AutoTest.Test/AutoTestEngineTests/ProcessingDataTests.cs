using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoTestEngine;
using System.Reflection;


namespace AutoTest.Test.AutoTestEngineTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProcessingDataTests
    {
        [TestMethod]
        public void ProcessingDataEntryInitTest()
        {
            var entryModel = DataHelper.MathPowerData.Entry;
            var procData = new ProccessingData(entryModel);

            Assert.IsTrue(procData.BoundaryType == BoundaryType.Entry);
            Assert.IsTrue(procData.Exception == null);
            Assert.IsTrue(procData.Method != null);
            Assert.IsTrue(procData.ReturnType != null);
            Assert.IsTrue(procData.ReturnValue == null);
            Assert.IsTrue(procData.TargetInstance == null);
        }

        [TestMethod]
        public void ProcessingDataExceptionInitTest()
        {
            var exceptionModel = DataHelper.MathPowerData.Exception;
            var procData = new ProccessingData(exceptionModel);

            Assert.IsTrue(procData.BoundaryType == BoundaryType.Exception);
            Assert.IsTrue(procData.Exception != null);
            Assert.IsTrue(procData.Method != null);
            Assert.IsTrue(procData.ReturnType != null);
            Assert.IsTrue(procData.ReturnValue == null);
            Assert.IsTrue(procData.TargetInstance == null);
        }

        [TestMethod]
        public void ProcessingDataExitInitTest()
        {
            var exitModel = DataHelper.MathPowerData.Exit;
            var procData = new ProccessingData(exitModel);

            Assert.IsTrue(procData.BoundaryType == BoundaryType.Exit);
            Assert.IsTrue(procData.Exception == null);
            Assert.IsTrue(procData.Method != null);
            Assert.IsTrue(procData.ReturnType != null);
            Assert.IsTrue(procData.ReturnValue != null);
            Assert.IsTrue(procData.TargetInstance == null);
        }
    }
}
