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
                    var entryModel = new InterceptionEntryModel(DateTime.Now, new List<object>() { 2.0 }, GetMethodBase());
                    return entryModel;
                }
            }

            public static InterceptionExitModel Exit
            {
                get
                {
                    var exitModel = new InterceptionExitModel(DateTime.Now, 9.0, GetMethodBase());
                    return exitModel;
                }
            }

            public static InterceptionExceptionModel Exception
            {
                get
                {
                    var exceptionModel = new InterceptionExceptionModel(DateTime.Now, GetMethodBase(), new ArgumentNullException());
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
                    var entryModel = new InterceptionEntryModel(null, new List<object>() { 3.0, 2.0 }, GetMethodBase());
                    return entryModel;
                }
            }

            public static InterceptionExitModel Exit
            {
                get
                {
                    var exitModel = new InterceptionExitModel(null, 9.0, GetMethodBase());
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
