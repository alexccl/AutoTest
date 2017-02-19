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
        #region unserializable type
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
        #endregion

        [TestMethod]
        public void DAL_Helper_Test_Adding_Method_Adds_To_DB()
        {
            var mock = new Mock<IDAL>();
            mock.Setup(x => x.Fetch<RecordedMethod>(It.IsAny<Func<RecordedMethod, bool>>())).Returns(new List<RecordedMethod>());
            var helper = new RecordedMethodHelper(mock.Object);
            helper.AddRecordedMethod(new RecordedMethod(Guid.NewGuid()));
            mock.Verify(x => x.Create<RecordedMethod>(It.IsAny<RecordedMethod>()), Times.Once);
        }

        [TestMethod]
        public void DAL_Helper_Test_All_Results_Are_Returned()
        {
            var mock = new Mock<IDAL>();
            var methods = new List<RecordedMethod>() { new RecordedMethod(Guid.NewGuid()), new RecordedMethod(Guid.NewGuid()) };

            mock.Setup(x => x.Fetch<RecordedMethod>(It.IsAny<Func<RecordedMethod, bool>>()))
                .Callback((Func<RecordedMethod, bool> func) =>
                            methods = methods.Where(func).ToList())
                .Returns(methods);

            var helper = new RecordedMethodHelper(mock.Object);
            var res = helper.GetAllRecordedMethods();
            Assert.IsTrue(res.Count == 2);
        }

        [TestMethod]
        public void DAL_Helper_Test_Specific_ID_Is_Returned()
        {
            var mock = new Mock<IDAL>();
            var queryId = Guid.NewGuid();
            var methods = new List<RecordedMethod>() {
                new RecordedMethod(queryId), new RecordedMethod(Guid.NewGuid()) };

            mock.Setup(x => x.Fetch<RecordedMethod>(It.IsAny<Func<RecordedMethod, bool>>()))
                .Callback((Func<RecordedMethod, bool> func) =>
                            methods = methods.Where(func).ToList())
                .Returns(methods);

            var helper = new RecordedMethodHelper(mock.Object);
            var res = helper.GetMethodWithId(queryId);
            Assert.IsTrue(res.Identifier == queryId);
        }

        [TestMethod]
        public void DAL_Helper_Test_No_Id_Found_Returns_Null()
        {
            var mock = new Mock<IDAL>();
            var queryId = Guid.NewGuid();
            var methods = new List<RecordedMethod>() {
                new RecordedMethod(Guid.NewGuid()), new RecordedMethod(Guid.NewGuid()) };

            mock.Setup(x => x.Fetch<RecordedMethod>(It.IsAny<Func<RecordedMethod, bool>>()))
                .Returns(new List<RecordedMethod>());

            var helper = new RecordedMethodHelper(mock.Object);
            var res = helper.GetMethodWithId(queryId);
            Assert.IsTrue(res == null);
        }

        [TestMethod]
        public void DAL_Helper_Test_No_Duplicate_Id()
        {
            var mock = new Mock<IDAL>();
            var queryId = Guid.NewGuid();
            var methods = new List<RecordedMethod>() {
                new RecordedMethod(queryId), new RecordedMethod(Guid.NewGuid()) };

            mock.Setup(x => x.Fetch<RecordedMethod>(It.IsAny<Func<RecordedMethod, bool>>()))
                .Callback((Func<RecordedMethod, bool> func) =>
                            methods = methods.Where(func).ToList())
                .Returns(methods);

            var helper = new RecordedMethodHelper(mock.Object);
            helper.AddRecordedMethod(new RecordedMethod(queryId));

            mock.Verify(x => x.Remove<RecordedMethod>(It.IsAny<RecordedMethod>()), Times.Once);
        }
    }
}
