using AutoTestEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    static class DataHelper
    {
        public static class DateTimeAddDaysData
        {
            public static InterceptionEntryModel Entry
            {
                get
                {
                    var instanceTypeVal = DateTime.Now;
                    var argsTypeVal = new List<object>() { 2.0 };
                    var entryModel = new InterceptionEntryModel(instanceTypeVal, argsTypeVal, GetMethodBase());
                    return entryModel;
                }
            }

            public static InterceptionExitModel Exit
            {
                get
                {
                    var instance =  DateTime.Now;
                    var returnVal = 9.0;
                    var exitModel = new InterceptionExitModel(instance, returnVal, GetMethodBase());
                    return exitModel;
                }
            }

            public static InterceptionExceptionModel Exception
            {
                get
                {
                    var instance = DateTime.Now;
                    var exceptionModel = new InterceptionExceptionModel(instance, GetMethodBase(), new ArgumentNullException());
                    return exceptionModel;
                }
            }

            private static MethodBase GetMethodBase()
            {
                return typeof(DateTime).GetMethod("AddDays");
            }
        }

        public static class MathPowerData
        {
            public static InterceptionEntryModel Entry
            {
                get
                {
                    object instance = null;
                    var args = new List<object>() { 3.0,2.0 };
                    var entryModel = new InterceptionEntryModel(instance, args, GetMethodBase());
                    return entryModel;
                }
            }

            public static InterceptionExitModel Exit
            {
                get
                {
                    object instance = null;
                    var returnVal =  9.0;
                    var exitModel = new InterceptionExitModel(instance, returnVal, GetMethodBase());
                    return exitModel;
                }
            }

            public static InterceptionExceptionModel Exception
            {
                get
                {
                    var exceptionModel = new InterceptionExceptionModel(null, GetMethodBase(), new ArgumentNullException());
                    return exceptionModel;
                }
            }


            private static MethodBase GetMethodBase()
            {
                return typeof(Math).GetMethod("Pow", // Name of the method
                                                        BindingFlags.Static | BindingFlags.Public, // We want a public static method
                                                        null,
                                                        new[] { typeof(double), typeof(double) }, // WriteLine(string, object[]),
                                                        null
                                                        );
            }
        }
    }
}
