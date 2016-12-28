using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class InterceptionEntryModel
    {
        /// <summary>
        /// Instance of the object from which the method is called, can be null for static methods
        /// </summary>
        public object TargetInstance { get; private set; }
        public List<object> MethodArgs { get; private set; }
        public MethodBase Method { get; private set; }

        public InterceptionEntryModel (object targetInstance, List<object> methodArgs, MethodBase method)
        {
            this.TargetInstance = targetInstance;
            this.MethodArgs = methodArgs;
            this.Method = method;
        }
    }
}
