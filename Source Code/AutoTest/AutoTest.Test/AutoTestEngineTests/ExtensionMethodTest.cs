using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTestEngine.Helpers;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class ExtensionMethodTest
    {
        [TestMethod]
        public void To_Type_Val_Null_List_Test()
        {
            List<object> list = null;
            var result = list.ToTypeValList();

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void To_Type_Val_Empty_List_Test()
        {
            List<object> list = new List<object>();
            var result = list.ToTypeValList();

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void To_Type_Val_Filled_List_Test()
        {
            var stringVal = "blah";
            var floatVal = 3.14f;
            var dateVal = DateTime.Now;

            List<object> list = new List<object>() {stringVal, floatVal, dateVal };
            var result = list.ToTypeValList();

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.Count == 3);

            Assert.IsTrue(result[0].Type.Equals(typeof(string)));
            Assert.IsTrue(result[0].Value.Equals(stringVal));

            Assert.IsTrue(result[1].Type.Equals(typeof(float)));
            Assert.IsTrue(result[1].Value.Equals(floatVal));

            Assert.IsTrue(result[2].Type.Equals(typeof(DateTime)));
            Assert.IsTrue(result[2].Value.Equals(dateVal));
        }

    }
}
