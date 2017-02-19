using AutoTestEngine;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTest4Unity
{
    public class AutoTestBehavior : IInterceptionBehavior
    {
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
            var engine = new Engine(new EngineConfiguration());
            var args = new List<object>();
            for(int i = 0; i < input.Arguments.Count; i++)
            {
                args.Add(input.Arguments[i]);
            }

            var entryModel = new InterceptionEntryModel(input.Target, args, input.MethodBase);
            engine.OnEntry(entryModel);
            IMethodReturn result = getNext()(input, getNext);
            if(result.Exception != null)
            {
                var exceptionModel = new InterceptionExceptionModel(input.Target, input.MethodBase, result.Exception);
                engine.OnException(exceptionModel);
            }
            else
            {
                var exitModel = new InterceptionExitModel(input.Target, result.ReturnValue, input.MethodBase);
                engine.OnExit(exitModel);
            }

            return result;
        }
    }
}
