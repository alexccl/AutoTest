using AutoTestEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder
{
    internal class MethodMetaData
    {
        public string MethodName { get; set; }
        public bool IsGeneric
        {
            get
            {
                return TypeParameters.Count != 0;
            }
            set { }
        }

        public MethodMetaData() { }

        public MethodMetaData(MethodBase method)
        {
            this.MethodName = method.Name;

            this.TypeParameters = new List<Type>();
            if (method.IsGenericMethod)
            {
                this.TypeParameters.AddRange(method.GetGenericArguments().ToList());
            }
        }

        public List<Type> TypeParameters { get; set; }

        public string GetFriendlyName()
        {
            if(!IsGeneric)
            {
                return MethodName;
            }

            var genericString =  MethodName + "<" + TypeParameters.First().GetFriendlyName();
            for(int i = 1; i < TypeParameters.Count; i++)
            {
                genericString += ", " + TypeParameters[i].GetFriendlyName();
            }

            genericString += ">";
            return genericString;
        }
    }
}
