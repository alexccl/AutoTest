using AutoTestEngine;
using AutoTestEngine.DAL.TexFileImplementation;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest4Unity
{
    public class AutoTestBehavior : IInterceptionBehavior
    {
        private static int _stackDepth = 0;
        private static Engine engine = new Engine(new EngineConfiguration());
        bool IInterceptionBehavior.WillExecute
        {
            get
            {
                return true;
            }
        }

        IEnumerable<Type> IInterceptionBehavior.GetRequiredInterfaces()
        {
            return Enumerable.Empty<Type>();
        }

        IMethodReturn IInterceptionBehavior.Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {

            ProcessContextEntry();
            

            var targetType = input.Target.GetType() ?? input.MethodBase.DeclaringType;
            var targetVal = input.Target;

            var args = new List<object>();
            for(int i = 0; i < input.Arguments.Count; i++)
            {
                args.Add(input.Arguments[i]);
            }

            var entryModel = new InterceptionEntryModel(targetVal, args, input.MethodBase);
            engine.OnEntry(entryModel);
            IMethodReturn result = getNext()(input, getNext);

            targetVal = input.Target;
            if (result.Exception != null)
            {
                var exceptionModel = new InterceptionExceptionModel(targetVal, input.MethodBase, result.Exception);
                engine.OnException(exceptionModel);
            }
            else
            {
                var exitModel = new InterceptionExitModel(targetVal, result.ReturnValue, input.MethodBase);
                engine.OnExit(exitModel);
            }
            ProcessContextExit();

            return result;
        }

        private void ProcessContextExit()
        {
            _stackDepth--;
            if(_stackDepth == 0)
            {
                (new Engine(new EngineConfiguration())).GenerateTests();
            }
        }

        private void ProcessContextEntry()
        {
            _stackDepth++;
        }
    }
}
