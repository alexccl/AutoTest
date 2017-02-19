using AutoTestEngine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class EntryProcessingResultTest
    {
        [TestMethod]
        public void Entry_Processing_Result_Test_Obj_Init()
        {
            var procResult = new EntryProcessingResult();
            Assert.IsFalse(procResult.BypassProxiedMethod);
            Assert.IsNull(procResult.BypassProxiedMethodValue);
        }
    }
}
