using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.Helpers.Serialization
{
    internal class SerializationResult
    {
        public bool Success { get; private set; }
        public Exception FailureException { get; private set; }
        public Type SerializedType { get; private set; }
        public string Result { get; private set; }

        public static SerializationResult InitSuccessfulSerialization(object originalObject, string result, Type t)
        {
            return new SerializationResult(originalObject, result, t);
        }

        public static SerializationResult InitFailedSerialization(object originalObject, Exception ex)
        {
            return new SerializationResult(originalObject, ex);
        }

        /// <summary>
        /// Initialize a successful result
        /// </summary>
        /// <param name="originalObject">the object that was serialized</param>
        /// <param name="result">the result of the serialization</param>
        private SerializationResult(object originalObject, string result, Type t)
        {
            this.Success = true;
            this.FailureException = null;
            this.SerializedType = t;
            this.Result = result;
        }

        /// <summary>
        /// Initializes a failure result
        /// </summary>
        /// <param name="originalObject">the object that tried to be serialized</param>
        /// <param name="ex">the exception thrown during serialization</param>
        private SerializationResult(object originalObject, Exception ex)
        {
            this.Success = false;
            this.FailureException = ex;
            this.SerializedType = originalObject.GetType();
            this.Result = null;
        }
    }
}
