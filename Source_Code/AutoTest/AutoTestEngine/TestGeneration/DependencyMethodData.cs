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
        public MethodMetaData MethodData { get; set; }
        public List<Type> MethodArgs { get; set; }
        
        public List<MethodCallReturnData> MethodCallReturs { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subMethods">Must be same methods on same type</param>
        public DependencyMethodData(List<RecordedSubMethod> subMethods)
        {
            this.MethodData = subMethods.FirstOrDefault().MethodData;
            this.MethodArgs = subMethods.FirstOrDefault().Args.Select(x => x?.GetType()).ToList();

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
                    retData = new MethodCallReturnData(subMethod.ReturnVal);
                }

                this.MethodCallReturs.Add(retData);
                
            }
        }
    }
}
