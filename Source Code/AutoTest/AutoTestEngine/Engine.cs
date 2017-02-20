using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class Engine : IEngine
    {
        private EngineConfiguration _configuration;
        private IEngineImplementation _engineImp;
        private StandardKernel _kernel;

        public Engine(EngineConfiguration configuration)
        {
            _configuration = configuration;
            IOCInit();
            _engineImp = _kernel.Get<IEngineImplementation>();
        }

        public EntryProcessingResult OnEntry(InterceptionEntryModel entryModel)
        {
            var procData = new InterceptionProcessingData(entryModel, _configuration);
            var engineRes = _engineImp.RunEngine(procData);

            var returnVal = new EntryProcessingResult();

            if(engineRes.OverrideValue != null)
            {
                returnVal.BypassProxiedMethod = true;
                returnVal.BypassProxiedMethodValue = engineRes.OverrideValue;
            }

            return returnVal;
        }


        public void OnException(InterceptionExceptionModel exceptionModel)
        {
            var procData = new InterceptionProcessingData(exceptionModel, _configuration);
            _engineImp.RunEngine(procData);
        }

        public void OnExit(InterceptionExitModel exitModel)
        {
            var procData = new InterceptionProcessingData(exitModel, _configuration);
            _engineImp.RunEngine(procData);
        }

        /// <summary>
        /// Initializes the IOC container
        /// </summary>
        private void IOCInit()
        {
            var settings = new NinjectSettings
            {
                InjectNonPublic = true
            };
            _kernel = new StandardKernel(settings);
            _kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}
