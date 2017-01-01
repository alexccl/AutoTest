using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoTestEngine.DAL.TexFileImplementation;
using Moq;
using System.Collections.Generic;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class TextFileDALTest
    {
        [TestMethod]
        public void TextFileDALAddTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetTypeRepostiory<string>()).Returns(new List<string>() { "foo" });
            var dal = new TextFileDAL(mock.Object);
            dal.Create<string>("bar");

            var newRep = new List<string>() { "foo", "bar" };
            mock.Verify(x => x.SetTypeRepository<string>(newRep));
        }

        [TestMethod]
        public void TextFileDALRemoveTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetTypeRepostiory<string>()).Returns(new List<string>() { "foo", "bar" });
            var dal = new TextFileDAL(mock.Object);
            dal.Remove<string>("foo");

            var newRep = new List<string>() { "bar" };
            mock.Verify(x => x.SetTypeRepository<string>(newRep));
        }

        [TestMethod]
        public void TextFileDALQueryTest()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(x => x.GetTypeRepostiory<string>()).Returns(new List<string>() { "foo", "bar" });
            var dal = new TextFileDAL(mock.Object);
            var res = dal.Fetch<string>(x => x.Contains("ar"));

            Assert.IsTrue(res.Count == 1 && res[0] == "bar");
        }

        [TestMethod]
        public void TextFileDALCommitTest()
        {
            var mock = new Mock<IRepository>();
            var dal = new TextFileDAL(mock.Object);


            dal.CommitChanges();
            mock.Verify(x => x.CommitChanges(), Times.Exactly(1));
        }


    }
}
