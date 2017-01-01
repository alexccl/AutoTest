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
        public Engine(EngineConfiguration configuration)
        {
            _configuration = configuration;
            IOCInit();
        }

        public EntryProcessingResult OnEntry(InterceptionEntryModel entryModel)
        {
            throw new NotImplementedException();
        }

        public void OnException(InterceptionExceptionModel exceptionModel)
        {
            throw new NotImplementedException();
        }

        public void OnExit(InterceptionExitModel exitModel)
        {
            throw new NotImplementedException();
        }

        private void IOCInit()
        {
            var settings = new NinjectSettings
            {
                InjectNonPublic = true
            };
            var kernel = new StandardKernel(settings);
            kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}
