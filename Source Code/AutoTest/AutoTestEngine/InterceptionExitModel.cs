using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class InterceptionExitModel
    {
        /// <summary>
        /// Instance of the object from which the method is called, can be null for static methods
        /// </summary>
        public object TargetInstace { get; set; }
        public object ReturnValue { get; set; }
        public MethodBase Method { get; set; }

        public InterceptionExitModel(object targetInstance, object returnValue, MethodBase method)
        {
            this.TargetInstace = targetInstance;
            this.ReturnValue = returnValue;
            this.Method = method;
        }
    }
}
