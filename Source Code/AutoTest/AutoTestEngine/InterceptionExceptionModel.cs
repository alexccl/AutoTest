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
        public TypeValModel TargetInstance { get; set; }

        /// <summary>
        /// Method metadata of called method
        /// </summary>
        public MethodBase Method { get; set; }

        /// <summary>
        /// Instance of the exception being thrown
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetInstance">Instance of the object from which the method is called, can be null for static methods</param>
        /// <param name="method">Method metadata of called method</param>
        /// <param name="exception">Instance of the exception being thrown</param>
        public InterceptionExceptionModel(TypeValModel targetInstance, MethodBase method, Exception exception)
        {
            this.TargetInstance = targetInstance;
            this.Method = method;
            this.Exception = exception;
        }


    }
}
