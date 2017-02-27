using AutoTestEngine.DAL.Models;
using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using KellermanSoftware.CompareNetObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    internal class SingleTest
    {
        public string TestName { get; set; }
        public string ObjectInstance { get; set; }
        public Type InstanceType { get; set; }
        public List<DependencyData> Dependencies { get; set; }
        public List<SerializedArg> Args { get; private set; }
        public MethodMetaData MethodData { get; private set; }
        public bool WasExceptionThrown {
            get
            {
                return ThrownException != null;

            }
        }
        public Exception ThrownException { get; private set; }

        public TypeValModel ReturnVal{ get; private set; }
        public string SerializedReturnVal { get; private set; }

        private Random _rand;

        public SingleTest() {
            _rand = new Random(DateTime.Now.Millisecond);
        }
        public SingleTest(RecordedMethod method) : this()
        {
            this.TestName = $"{method.TargetType.Name}_{method.MethodData.MethodName}_{this.GenRandIntString()}";
            this.ObjectInstance = method.InstanceAtExecutionTime.Value;
            this.InstanceType = method.InstanceAtExecutionTime.Type;
            this.ThrownException = method.MethodException;
            this.ReturnVal = method.ReturnTypeVal;
            this.MethodData = method.MethodData;

            if (!this.WasExceptionThrown)
            {
                this.SerializedReturnVal = JsonConvert.SerializeObject(method.ReturnTypeVal.Value);
            }

            this.Args = new List<SerializedArg>();
            for(int i = 0; i < method.Args.Count; i++)
            {
                var arg = method.Args[i];
                var name = $"arg_{arg.Type.Name}_{i}";
                var serVal = JsonConvert.SerializeObject(arg.Value);
                this.Args.Add(new SerializedArg(arg.Type, name, serVal));
            }

            this.Dependencies = new List<DependencyData>();
            var groupings = method.SubMethods.GroupBy(x => x.TargetType);
            foreach(var grouping in groupings)
            {
                var dependency = new DependencyData(grouping.ToList());
                this.Dependencies.Add(dependency);
            }
        }

        private string GenRandIntString()
        {
            return _rand.Next(int.MaxValue).ToString();
        }
    }
}
