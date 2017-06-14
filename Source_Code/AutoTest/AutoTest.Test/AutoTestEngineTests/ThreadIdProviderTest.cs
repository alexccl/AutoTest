using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class ThreadIdProviderTest
    {
        [TestMethod]
        public void Thread_Id_Provider_Same_Thread_Test()
        {
            var provider = new ThreadIdProvider();
            int id1 = provider.GetThreadId();
            int id2 = provider.GetThreadId();

            Assert.IsTrue(id1 == id2);
        }

        [TestMethod]
        public void Thread_Id_Provider_Same_Thread_Dif_Instance()
        {
            var provider1 = new ThreadIdProvider();
            var provider2 = new ThreadIdProvider();

            var id1 = provider1.GetThreadId();
            var id2 = provider2.GetThreadId();

            Assert.IsTrue(id1 == id2);
        }

        [TestMethod]
        public void Thread_Id_Provider_Dif_Thread_Dif_Id()
        {
            var id1 = (new ThreadIdProvider()).GetThreadId();

            int id2 = GetThreadIdFromAnotherThread();

            Assert.IsFalse(id1 == id2);
        }

        [TestMethod]
        public void Thread_Id_Provider_Same_Provider_Instance_Dif_Thread()
        {
            var provider = new ThreadIdProvider();

            var id1 = GetThreadIdFromAnotherThread(provider);

            int id2 = GetThreadIdFromAnotherThread(provider);

            Assert.IsFalse(id1 == id2);
        }




        private int GetThreadIdFromAnotherThread()
        {
            return this.GetThreadIdFromAnotherThread(new ThreadIdProvider());
        }

        private int GetThreadIdFromAnotherThread(ThreadIdProvider provider)
        {
            int id = 0;
            Thread thread = new Thread(delegate ()
            {
                //Do somthing and set your value
                id = provider.GetThreadId();
            });

            thread.Start();

            while (thread.IsAlive)
                Thread.Sleep(1);

            return id;
        }
    }
}
