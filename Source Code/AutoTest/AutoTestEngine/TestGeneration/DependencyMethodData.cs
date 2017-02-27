using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    class DependencyMethodData
    {
        public string MethodName { get; set; }
        public List<Type> MethodArgs { get; set; }
        
        public List<MethodCallReturnData> MethodCallReturs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subMethods">Must be same methods on same type</param>
        public DependencyMethodData(List<RecordedSubMethod> subMethods)
        {
            this.MethodName = subMethods.FirstOrDefault().MethodName;
            this.MethodArgs = subMethods.FirstOrDefault().Args.Select(x => x.Type).ToList();

            this.MethodCallReturs = new List<MethodCallReturnData>();
            foreach(var subMethod in subMethods)
            {
                MethodCallReturnData retData = null;
                if(subMethod.MethodException != null)
                {
                    retData = new MethodCallReturnData(subMethod.MethodException);
                }
                else
                {
                    retData = new MethodCallReturnData(subMethod.ReturnTypeVal);
                }

                this.MethodCallReturs.Add(retData);
                
            }
        }
    }
}
