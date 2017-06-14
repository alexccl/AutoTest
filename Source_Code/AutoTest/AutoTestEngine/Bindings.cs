using AutoTestEngine.DAL;
using AutoTestEngine.DAL.Helpers;
using AutoTestEngine.DAL.TexFileImplementation;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.InterceptionVerification;
using AutoTestEngine.InterceptionVerification.Verifiers;
using AutoTestEngine.ProcessMultiplexer;
using AutoTestEngine.ProcessMultiplexer.Processes;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder.ExecutionCache;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// used by ninject to bind all interfaces to their default implenentations for ninject's IOC container
    /// </summary>
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IEngineImplementation>().To<EngineImplementation>();
            Bind<IProcessMultiplexer>().To<ProcessMultiplexer.ProcessMultiplexer>();
            Bind<IRecordingMethodManager>().To<RecordingMethodManager>();
            Bind<IThreadIdProvider>().To<ThreadIdProvider>();
            Bind<IExecutionCache>().To<ExecutionCache>();
            Bind<IExecutionStack>().To<ExecutionStack>();
            Bind<IVerificationPipeline>().To<VerificationPipeline>();
            Bind<ISerializationHelper>().To<SerializationHelper>();
            Bind<IRepository>().To<Repository>();
            Bind<IRecordedMethodHelper>().To<RecordedMethodHelper>();
            Bind<IUnserializableTypeHelper>().To<UnserializableTypeHelper>();
            Bind<IDAL>().To<TextFileDAL>();

            //interface array injections
            Bind<IProcess>().To<ExecutionRecorderProcess>();

            Bind<IVerifier>().To<InputParameterVerifier>();
            Bind<IVerifier>().To<InstanceSerializationVerifier>();
            Bind<IVerifier>().To<ReturnValVerifier>();
        }
    }
}
