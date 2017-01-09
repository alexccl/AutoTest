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
    class Bindings : NinjectModule
    {
        public override void Load()
        {
            throw new NotImplementedException();
        }
    }
}
