using AutoTestEngine.Helpers.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    internal class MethodCallReturnData
    {
        private ISerializationHelper _serialier = new SerializationHelper();
        public bool ExceptionThrown { get
            {
                return this.Exception != null;
            } }
        public Exception Exception { get; set; }
        public object ReturnVal { get; set; }

        public string SerializedValue
        {
            get
            {
                return _serialier.Serialize(this.ReturnVal).SerializedValue.Value;
            }
        }

        public string SerializedException
        {
            get
            {
                var ex = _serialier.Serialize(this.Exception).SerializedValue.Value;
                return ex;
            }
        }

        public Type ExceptionType
        {
            get
            {
                return this.Exception?.GetType();
            }
        }

        public MethodCallReturnData() { }

        public MethodCallReturnData(object returnVal)
        {
            this.ReturnVal = returnVal;
        }

        public MethodCallReturnData(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
