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

        /// <summary>
        /// The value being returned by the proxied class invocation
        /// </summary>
        public object ReturnValue { get; set; }

        /// <summary>
        /// Method metadata of called method
        /// </summary>
        public MethodBase Method { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetInstance">Instance of the object from which the method is called, can be null for static methods</param>
        /// <param name="returnValue">The value being returned by the proxied class invocation</param>
        /// <param name="method">Method metadata of called method</param>
        public InterceptionExitModel(object targetInstance, object returnValue, MethodBase method)
        {
            this.TargetInstace = targetInstance;
            this.ReturnValue = returnValue;
            this.Method = method;
        }
    }
}
