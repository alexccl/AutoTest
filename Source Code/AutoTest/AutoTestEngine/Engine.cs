using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class Engine
    {
        public Engine()
        {
            IOCInit();

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
