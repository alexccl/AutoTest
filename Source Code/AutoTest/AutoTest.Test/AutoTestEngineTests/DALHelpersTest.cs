using AutoTestEngine.DAL;
using AutoTestEngine.DAL.Helpers;
using AutoTestEngine.DAL.Models;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;


namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class DALHelpersTest
    {
        [TestMethod]
        public void DAL_Helper_Test_IsUnserializable_Returns_False_When_Not_In_DB()
        {
            var mock = new Mock<IDAL>();
            mock.Setup(x => x.Fetch<UnserializableType>(It.IsAny<Func<UnserializableType, bool>>()))
                .Returns(new List<UnserializableType>());

            var helper = new UnserializableTypeHelper(mock.Object);

            var result = helper.IsUnserializable(typeof(double));

            Assert.IsTrue(result == false);
        }

        [TestMethod]
        public void DAL_Helper_Test_IsUnserializable_Returns_False_When_In_DB()
        {
            var mock = new Mock<IDAL>();
            var unserializableTypes = new List<UnserializableType>() { new UnserializableType(typeof(int)), new UnserializableType(typeof(double)) };
            mock.Setup(x => x.Fetch<UnserializableType>(It.IsAny<Func<UnserializableType, bool>>()))
                .Returns(new List<UnserializableType>() { new UnserializableType(typeof(double)) });

            var helper = new UnserializableTypeHelper(mock.Object);

            var result = helper.IsUnserializable(typeof(double));

            Assert.IsTrue(result == true);
        }

        [TestMethod]
        public void DAL_Helper_Test_Adding_Type_Adds_To_DB()
        {
            var mock = new Mock<IDAL>();

            mock.Setup(x => x.Fetch<UnserializableType>(It.IsAny<Func<UnserializableType, bool>>()))
                .Returns(new List<UnserializableType>());

            var helper = new UnserializableTypeHelper(mock.Object);

            helper.AddUnserializableType(typeof(double));

            mock.Verify(x => x.Create<UnserializableType>(It.IsAny<UnserializableType>()), Times.Once);
        }

        [TestMethod]
        public void DAL_Helper_Test_Adding_Type_Does_Not_Add_To_DB_When_Already_Exists()
        {
            var mock = new Mock<IDAL>();

            mock.Setup(x => x.Fetch<UnserializableType>(It.IsAny<Func<UnserializableType, bool>>()))
                .Returns(new List<UnserializableType>() { new UnserializableType(typeof(double)) });


            var helper = new UnserializableTypeHelper(mock.Object);

            helper.AddUnserializableType(typeof(double));

            mock.Verify(x => x.Create<UnserializableType>(It.IsAny<UnserializableType>()), Times.Never);
        }
    }
}
