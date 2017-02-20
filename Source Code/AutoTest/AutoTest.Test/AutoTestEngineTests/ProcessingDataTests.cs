using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoTestEngine;
using System.Reflection;
using AutoTestEngine.Attributes;
using System.Diagnostics.Contracts;

namespace AutoTest.Test.AutoTestEngineTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProcessingDataTests
    {
        private EngineConfiguration _config = new EngineConfiguration();
        private TypeValModel _instance;
        private TypeValModel _returnVal;
        private List<TypeValModel> _args;

        [TestInitialize]
        public void Setup()
        {
            _instance = new TypeValModel(typeof(TestClass), new TestClass());
            _args = new List<TypeValModel>();
            _returnVal = new TypeValModel(typeof(int), 1);
        }

        [TestMethod]
        public void ProcessingDataEntryInitTest()
        {
            var entryModel = DataHelper.MathPowerData.Entry;
            var procData = new InterceptionProcessingData(entryModel, _config);

            Assert.IsTrue(procData.BoundaryType == BoundaryType.Entry);
            Assert.IsTrue(procData.Exception == null);
            Assert.IsTrue(procData.Method != null);
            Assert.IsTrue(procData.ReturnType != null);
            Assert.IsTrue(procData.ReturnValue == null);
            Assert.IsTrue(procData.TargetInstance.Value == null);
            Assert.IsTrue(procData.VerificationFailures != null);
        }

        [TestMethod]
        public void ProcessingDataExceptionInitTest()
        {
            var exceptionModel = DataHelper.MathPowerData.Exception;
            var procData = new InterceptionProcessingData(exceptionModel, _config);

            Assert.IsTrue(procData.BoundaryType == BoundaryType.Exception);
            Assert.IsTrue(procData.Exception != null);
            Assert.IsTrue(procData.Method != null);
            Assert.IsTrue(procData.ReturnType != null);
            Assert.IsTrue(procData.ReturnValue == null);
            Assert.IsTrue(procData.TargetInstance == null);
            Assert.IsTrue(procData.VerificationFailures != null);
        }

        [TestMethod]
        public void ProcessingDataExitInitTest()
        {
            var exitModel = DataHelper.MathPowerData.Exit;
            var procData = new InterceptionProcessingData(exitModel, _config);

            Assert.IsTrue(procData.BoundaryType == BoundaryType.Exit);
            Assert.IsTrue(procData.Exception == null);
            Assert.IsTrue(procData.Method != null);
            Assert.IsTrue(procData.ReturnType != null);
            Assert.IsTrue(procData.ReturnValue != null);
            Assert.IsTrue(procData.TargetInstance.Value == null);
            Assert.IsTrue(procData.VerificationFailures != null);
        }

        [TestMethod]
        public void Processing_Data_Test_Class_Level_Attributes()
        {
            var entry = new InterceptionEntryModel(_instance, _args, GetAttMethodBase());
            var processingModel = new InterceptionProcessingData(entry, new EngineConfiguration());

            Assert.IsTrue(processingModel.ClassAttributes.Count == 1);
            Assert.IsTrue(processingModel.ClassAttributes.Contains(typeof(AutoTestEngine.Attributes.AutoTest)));
            
        }


        [TestMethod]
        public void Processing_Data_Test_Class_Method_Level_Attributes()
        {
            var entry = new InterceptionEntryModel(_instance, _args, GetAttMethodBase());
            var processingModel = new InterceptionProcessingData(entry, new EngineConfiguration());

            Assert.IsTrue(processingModel.MethodAttributes.Count == 1);
            Assert.IsTrue(processingModel.MethodAttributes.Contains(typeof(AutoTestEngine.Attributes.AutoTest)));

            entry = new InterceptionEntryModel(_instance, _args, GetNonAttMethodBase());
            processingModel = new InterceptionProcessingData(entry, new EngineConfiguration());

            Assert.IsTrue(processingModel.MethodAttributes.Count == 0);
        }

        [TestMethod]
        public void Processing_Data_Test_All_Constructors()
        {
            var entry = new InterceptionEntryModel(_instance, _args, GetAttMethodBase());
            var processingModel = new InterceptionProcessingData(entry, new EngineConfiguration());

            Assert.IsTrue(processingModel.MethodAttributes.Count == 1);
            Assert.IsTrue(processingModel.MethodAttributes.Contains(typeof(AutoTestEngine.Attributes.AutoTest)));


            var exit = new InterceptionExitModel(_instance, _returnVal, GetAttMethodBase());
            processingModel = new InterceptionProcessingData(exit, new EngineConfiguration());

            Assert.IsTrue(processingModel.MethodAttributes.Count == 1);
            Assert.IsTrue(processingModel.MethodAttributes.Contains(typeof(AutoTestEngine.Attributes.AutoTest)));

            var exception = new InterceptionExceptionModel(_instance, GetAttMethodBase(), new Exception());
            processingModel = new InterceptionProcessingData(exception, new EngineConfiguration());
        }

        private MethodBase GetAttMethodBase()
        {
            var methodBase = typeof(TestClass).GetMethod("WithAtt");

            return methodBase;
        }

        private MethodBase GetNonAttMethodBase()
        {
            var methodBase = typeof(TestClass).GetMethod("WithoutAtt");

            return methodBase;
        }

        [Serializable]
        [AutoTestEngine.Attributes.AutoTest]
        class TestClass
        {
            [Pure]
            [AutoTestEngine.Attributes.AutoTest]
            public int WithAtt()
            {
                return 1;
            }

            [Pure]
            public string WithoutAtt()
            {
                return "foo";
            }
        }
    }
}
