using AutoTestEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    class TestClass
    {
        public string Method1(int x, DateTime y)
        {
            var subClass = new TestSubClass();
            if (subClass.Method1((float)x) < 0)
            {
                return "x less than 0";
            }
            return "x greater than 0";
        }

        private static MethodBase GetMethod1Base
        {
            get
            {
                return typeof(TestClass).GetMethod("Method1");
            }
        }

        public static InterceptionProcessingData Method1Entry
        {
            get
            {
                var entryModel = new InterceptionEntryModel((new TestClass()), new List<object>() { 1, DateTime.Now }, TestClass.GetMethod1Base);
                return new InterceptionProcessingData(entryModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exit
        {
            get
            {
                var exitModel = new InterceptionExitModel((new TestClass()), "blah", TestClass.GetMethod1Base);
                return new InterceptionProcessingData(exitModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exception
        {
            get
            {
                var exceptionModel = new InterceptionExceptionModel((new TestClass()), TestClass.GetMethod1Base, new Exception());
                return new InterceptionProcessingData(exceptionModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }
    }

    class TestSubClass
    {
        public int Method1(float x)
        {
            return Convert.ToInt32(x);
        }

        private static MethodBase GetMethod1Base
        {
            get
            {
                return typeof(TestSubClass).GetMethod("Method1");
            }
        }

        public static InterceptionProcessingData Method1Entry
        {
            get
            {
                var entryModel = new InterceptionEntryModel((new TestClass()), new List<object>() { 1.0}, TestSubClass.GetMethod1Base);
                return new InterceptionProcessingData(entryModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exit
        {
            get
            {
                var exitModel = new InterceptionExitModel((new TestClass()), 1, TestSubClass.GetMethod1Base);
                return new InterceptionProcessingData(exitModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exception
        {
            get
            {
                var exceptionModel = new InterceptionExceptionModel((new TestClass()), TestSubClass.GetMethod1Base, new Exception());
                return new InterceptionProcessingData(exceptionModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }
    }

    public static class ClassHelper
    {

    }
}
