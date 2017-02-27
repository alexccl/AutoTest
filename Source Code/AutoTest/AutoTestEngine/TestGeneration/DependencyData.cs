using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    internal class DependencyData
    {
        public Type MemberType { get; set; }
        public List<DependencyMethodData> Methods { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subMethods">MUST be methods on the same type</param>
        /// <param name="parentType"></param>
        public DependencyData(List<RecordedSubMethod> subMethods)
        {
            this.Methods = new List<DependencyMethodData>();
            //group methods by method name
            var groupings = subMethods.GroupBy(x => x.MethodName);

            foreach(var group in groupings)
            {
                var methodData = new DependencyMethodData(group.ToList());
                Methods.Add(methodData);
            }

            var distinctMethod = Methods.Select(x => x.MethodName);
            var parentInterfaces = subMethods.FirstOrDefault().TargetType.GetInterfaces();
            foreach(var iFace in parentInterfaces)
            {
                bool isMatch = true;
                foreach(var method in distinctMethod)
                {
                    if (!iFace.GetMethods().Any(x => x.Name == method)) isMatch = false;
                }
                
                if(isMatch)
                {
                    this.MemberType = iFace;
                    break;
                }
            }
        }
    }
}
