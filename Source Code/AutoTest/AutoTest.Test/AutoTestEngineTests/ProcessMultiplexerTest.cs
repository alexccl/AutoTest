using AutoTestEngine;
using AutoTestEngine.ProcessMultiplexer;
using AutoTestEngine.ProcessMultiplexer.Processes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest.Test.AutoTestEngineTests
{
    [TestClass]
    public class ProcessMultiplexerTest
    {
        [TestMethod]
        public void ProcessMultiplexerSingleSuccess()
        {
            var mock = new Mock<IProcess>();
            mock.Setup(x => x.ProcessPriority).Returns(1);
            mock.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);
            var procMult = new ProcessMultiplexer(new IProcess[] { mock.Object });

            procMult.Process(null);

            mock.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Once);
        }

        [TestMethod]
        public void ProcessMultiplexerSingleFailure()
        {
            var mock = new Mock<IProcess>();
            mock.Setup(x => x.ProcessPriority).Returns(1);
            mock.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(false);
            var procMult = new ProcessMultiplexer(new IProcess[] { mock.Object });

            procMult.Process(null);

            mock.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);
        }

        [TestMethod]
        public void ProcessMultiplexerOnlyOneProcessCalled()
        {
            var mock1 = new Mock<IProcess>();
            var mock2 = new Mock<IProcess>();

            mock1.Setup(x => x.ProcessPriority).Returns(1);
            mock2.Setup(x => x.ProcessPriority).Returns(2);

            mock1.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);
            mock2.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);

            var procMult = new ProcessMultiplexer(new IProcess[] { mock1.Object, mock2.Object });

            procMult.Process(null);

            mock1.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Once);
            mock2.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);
        }

        [TestMethod]
        public void ProcessMultiplexerProcessOrdering()
        {
            var mock1 = new Mock<IProcess>();
            var mock2 = new Mock<IProcess>();
            var mock3 = new Mock<IProcess>();

            mock1.Setup(x => x.ProcessPriority).Returns(1);
            mock2.Setup(x => x.ProcessPriority).Returns(2);
            mock3.Setup(x => x.ProcessPriority).Returns(3);

            mock1.Setup(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(new ProcessResult());
            mock1.Setup(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(new ProcessResult());
            mock1.Setup(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(new ProcessResult());

            //mock 1 should be called
            mock1.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);
            mock2.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);
            mock3.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);

            var procMult = new ProcessMultiplexer(new IProcess[] { mock3.Object, mock2.Object, mock1.Object });
            procMult.Process(null);
            mock1.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Once);
            mock2.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);
            mock3.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);

            //mock 2 should be called
            mock1.ResetCalls();
            mock2.ResetCalls();
            mock3.ResetCalls();
            mock1.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(false);
            mock2.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);
            mock3.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);

            procMult = new ProcessMultiplexer(new IProcess[] { mock3.Object, mock2.Object, mock1.Object });
            procMult.Process(null);
            mock1.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);
            mock2.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Once);
            mock3.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);

            //mock 3 should be called
            mock1.ResetCalls();
            mock2.ResetCalls();
            mock3.ResetCalls();
            mock1.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(false);
            mock2.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(false);
            mock3.Setup(x => x.ShouldExecuteProcess(It.IsAny<InterceptionProcessingModel>())).Returns(true);

            procMult = new ProcessMultiplexer(new IProcess[] { mock3.Object, mock2.Object, mock1.Object });
            procMult.Process(null);
            mock1.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);
            mock2.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Never);
            mock3.Verify(x => x.ExecuteProcess(It.IsAny<InterceptionProcessingModel>()), Times.Once);



        }
    }
}
