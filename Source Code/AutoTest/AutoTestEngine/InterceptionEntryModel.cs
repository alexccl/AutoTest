using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// Container for proxy entry data
    /// </summary>
    public class InterceptionEntryModel
    {
        /// <summary>
        /// Instance of the object from which the method is called, can be null for static methods
        /// </summary>
        public TypeValModel TargetInstance { get; private set; }

        /// <summary>
        /// Arguments passed to the method invocation
        /// </summary>
        public List<TypeValModel> MethodArgs { get; private set; }

        /// <summary>
        /// Method metadata of called method
        /// </summary>
        public MethodBase Method { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="targetInstance">Instance of the object from which the method is called, can be null for static methods</param>
        /// <param name="methodArgs">Arguments passed to the method invocation</param>
        /// <param name="method">Method metadata of called method</param>
        public InterceptionEntryModel (TypeValModel targetInstance, List<TypeValModel> methodArgs, MethodBase method)
        {
            this.TargetInstance = targetInstance;
            this.MethodArgs = methodArgs;
            this.Method = method;
        }
    }
}
