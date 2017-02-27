using AutoTestEngine;
using AutoTestEngine.DAL.Models;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{

    static class TestRecordedMethod
    {
        public static RecordedMethod Method1
        {
            get
            {
                var recordingMethod = TestClass.Method1EntryRecording;
                var subEntry = TestSubClass.Method1Entry;

                var subMethod = new RecordedSubMethod(Guid.NewGuid(), subEntry.TargetType, subEntry.MethodArgs, subEntry.ReturnType, subEntry.Method.Name);
                subMethod.CloseOutMethodWithReturnVal(1);
                recordingMethod.SubMethods.Add(subMethod);

                subMethod = new RecordedSubMethod(Guid.NewGuid(), subEntry.TargetType, subEntry.MethodArgs, subEntry.ReturnType, subEntry.Method.Name);
                subMethod.CloseOutMethodWithException(new NotImplementedException());
                recordingMethod.SubMethods.Add(subMethod);

                var subEntry2 = TestSubClass2.Method1Entry;
                var subMethod2 = new RecordedSubMethod(Guid.NewGuid(), subEntry2.TargetType, subEntry2.MethodArgs, subEntry2.ReturnType, subEntry2.Method.Name);
                subMethod2.CloseOutMethodWithReturnVal(2);
                recordingMethod.SubMethods.Add(subMethod2);

                recordingMethod.CloseOutMethodWithReturnVal("blah");

                var recordedMethod = new RecordedMethod(recordingMethod);
                return recordedMethod;
            }
        }
    }
    interface ITestClass
    {
        string Method1(int x, DateTime y);
    }

    class TestClass : ITestClass
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
                var instance = new TypeValModel(typeof(TestClass), new TestClass());
                var args = new List<TypeValModel>() {
                    new TypeValModel(typeof(int), 1),
                    new TypeValModel(typeof(DateTime), DateTime.Now)
                };

                var entryModel = new InterceptionEntryModel(instance, args, TestClass.GetMethod1Base);
                return new InterceptionProcessingData(entryModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static RecordingMethod Method1EntryRecording
        {
            get
            {
                var serInstance = new SerializedValue(Method1Entry.TargetType, "");
                return new RecordingMethod(Guid.NewGuid(), serInstance, Method1Entry.MethodArgs, Method1Entry.Method);
            }
        }

        public static InterceptionProcessingData Method1Exit
        {
            get
            {
                var instance = new TypeValModel(typeof(TestClass), new TestClass());
                var returnVal = new TypeValModel(typeof(string), "blah");
                var exitModel = new InterceptionExitModel(instance, returnVal, TestClass.GetMethod1Base);
                return new InterceptionProcessingData(exitModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exception
        {
            get
            {
                var instance = new TypeValModel(typeof(TestClass), new TestClass());
                var exceptionModel = new InterceptionExceptionModel(instance, TestClass.GetMethod1Base, new Exception());
                return new InterceptionProcessingData(exceptionModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }
    }

    interface ITestSubClass
    {
        int Method1(float x);
    }

    class TestSubClass : ITestSubClass
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
                var instance = new TypeValModel(typeof(TestSubClass), new TestSubClass());
                var args = new List<TypeValModel>() {
                    new TypeValModel(typeof(double), 1.0),
                };
                var entryModel = new InterceptionEntryModel(instance, args, TestSubClass.GetMethod1Base);
                return new InterceptionProcessingData(entryModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static RecordingMethod Method1EntryRecording
        {
            get
            {
                var serInstance = new SerializedValue(Method1Entry.TargetType, "");
                return new RecordingMethod(Guid.NewGuid(), serInstance, Method1Entry.MethodArgs, Method1Entry.Method);
            }
        }

        public static InterceptionProcessingData Method1Exit
        {
            get
            {
                var instance = new TypeValModel(typeof(TestSubClass), new TestSubClass());
                var returnVal = new TypeValModel(typeof(int), 1);
                var exitModel = new InterceptionExitModel(instance, returnVal, TestSubClass.GetMethod1Base);
                return new InterceptionProcessingData(exitModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exception
        {
            get
            {
                var instance = new TypeValModel(typeof(TestSubClass), new TestSubClass());
                var exceptionModel = new InterceptionExceptionModel(instance, TestSubClass.GetMethod1Base, new Exception());
                return new InterceptionProcessingData(exceptionModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }
    }

    interface ITestSubClass2
    {
        int Method1(double x);
    }

    class TestSubClass2 : IDisposable, ITestSubClass2, ICustomFormatter
    {
        public int Method1(double x)
        {
            return Convert.ToInt32(x);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }

        private static MethodBase GetMethod1Base
        {
            get
            {
                return typeof(TestSubClass2).GetMethod("Method1");
            }
        }

        public static InterceptionProcessingData Method1Entry
        {
            get
            {
                var instance = new TypeValModel(typeof(TestSubClass2), new TestSubClass2());
                var args = new List<TypeValModel>() {
                    new TypeValModel(typeof(double), 1.0),
                };
                var entryModel = new InterceptionEntryModel(instance, args, TestSubClass2.GetMethod1Base);
                return new InterceptionProcessingData(entryModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static RecordingMethod Method1EntryRecording
        {
            get
            {
                var serInstance = new SerializedValue(Method1Entry.TargetType, "");
                return new RecordingMethod(Guid.NewGuid(), serInstance, Method1Entry.MethodArgs, Method1Entry.Method);
            }
        }

        public static InterceptionProcessingData Method1Exit
        {
            get
            {
                var instance = new TypeValModel(typeof(TestSubClass), new TestSubClass());
                var returnVal = new TypeValModel(typeof(int), 1);
                var exitModel = new InterceptionExitModel(instance, returnVal, TestSubClass2.GetMethod1Base);
                return new InterceptionProcessingData(exitModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }

        public static InterceptionProcessingData Method1Exception
        {
            get
            {
                var instance = new TypeValModel(typeof(TestSubClass), new TestSubClass());
                var exceptionModel = new InterceptionExceptionModel(instance, TestSubClass2.GetMethod1Base, new Exception());
                return new InterceptionProcessingData(exceptionModel, new EngineConfiguration() { IsUnitTesting = false });
            }
        }
    }
}
