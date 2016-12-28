using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class InterceptionExceptionModel
    {
        /// <summary>
        /// Instance of the object from which the method is called, can be null for static methods
        /// </summary>
        public object TargetInstance { get; set; }
        public MethodBase Method { get; set; }
        public Exception Exception { get; set; }

        public InterceptionExceptionModel(object targetInstance, MethodBase method, Exception exception)
        {
            this.TargetInstance = targetInstance;
            this.Method = method;
            this.Exception = exception;
        }


    }
}
