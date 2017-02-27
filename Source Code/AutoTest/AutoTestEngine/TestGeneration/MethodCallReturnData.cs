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
        public bool ExceptionThrown { get
            {
                return this.Exception != null;
            } }
        public Exception Exception { get; set; }
        public TypeValModel ReturnVal { get; set; }

        public string SerializedValue
        {
            get
            {
                return JsonConvert.SerializeObject(ReturnVal.Value);
            }
        }

        public string SerializedException
        {
            get
            {
                var ex = JsonConvert.SerializeObject(this.Exception);
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

        public MethodCallReturnData(TypeValModel returnVal)
        {
            this.ReturnVal = returnVal;
        }

        public MethodCallReturnData(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
