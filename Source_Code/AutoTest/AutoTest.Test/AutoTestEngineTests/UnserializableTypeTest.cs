using AutoTestEngine.DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class UnserializableTypeTest
    {
        [TestMethod]
        public void Test_Unserializable_Test_Equality_Override()
        {
            var type1 = new UnserializableType(typeof(double));
            var type2 = new UnserializableType(typeof(string));
            var type3 = new UnserializableType((2.0).GetType());

            Assert.IsTrue(type1.Equals(type1));
            Assert.IsFalse(type1.Equals(type2));
            Assert.IsTrue(type1.Equals(type3));
        }
    }
}
