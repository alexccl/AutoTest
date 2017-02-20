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
                    var instanceTypeVal = new TypeValModel(typeof(DateTime), DateTime.Now);
                    var argsTypeVal = new List<TypeValModel>() { new TypeValModel(typeof(double), 2.0) };
                    var entryModel = new InterceptionEntryModel(instanceTypeVal, argsTypeVal, GetMethodBase());
                    return entryModel;
                }
            }

            public static InterceptionExitModel Exit
            {
                get
                {
                    var instanceTypeVal = new TypeValModel(typeof(DateTime), DateTime.Now);
                    var retTypeVal = new TypeValModel(typeof(double), 9.0);
                    var exitModel = new InterceptionExitModel(instanceTypeVal, retTypeVal, GetMethodBase());
                    return exitModel;
                }
            }

            public static InterceptionExceptionModel Exception
            {
                get
                {
                    var instanceTypeVal = new TypeValModel(typeof(DateTime), DateTime.Now);
                    var exceptionModel = new InterceptionExceptionModel(instanceTypeVal, GetMethodBase(), new ArgumentNullException());
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
                    var instanceTypeVal = new TypeValModel(typeof(Math), null);
                    var argsTypeVal = new List<TypeValModel>() { new TypeValModel(typeof(double), 3.0), new TypeValModel(typeof(double), 2.0) };
                    var entryModel = new InterceptionEntryModel(instanceTypeVal, argsTypeVal, GetMethodBase());
                    return entryModel;
                }
            }

            public static InterceptionExitModel Exit
            {
                get
                {
                    var instanceTypeVal = new TypeValModel(typeof(Math), null);
                    var retTypeVal = new TypeValModel(typeof(double), 9.0);
                    var exitModel = new InterceptionExitModel(instanceTypeVal, retTypeVal, GetMethodBase());
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
