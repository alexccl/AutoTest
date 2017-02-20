using AutoTestEngine;
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

            var targetType = input.MethodBase.DeclaringType;
            var targetVal = input.Target;
            var targetTypeVal = new TypeValModel(targetType, targetVal);

            var args = new List<TypeValModel>();
            for(int i = 0; i < input.Arguments.Count; i++)
            {
                var type = input.Arguments.GetParameterInfo(i).ParameterType;
                var val = input.Arguments[i];
                args.Add(new TypeValModel(type, val));
            }

            var entryModel = new InterceptionEntryModel(targetTypeVal, args, input.MethodBase);
            engine.OnEntry(entryModel);
            IMethodReturn result = getNext()(input, getNext);

            targetVal = input.Target;
            targetTypeVal = new TypeValModel(targetType, targetVal);
            if (result.Exception != null)
            {
                var exceptionModel = new InterceptionExceptionModel(targetTypeVal, input.MethodBase, result.Exception);
                engine.OnException(exceptionModel);
            }
            else
            {
                var returnType = ((MethodInfo)input.MethodBase).ReturnType;
                var returnVal = result.ReturnValue;
                var returnTypeVal = new TypeValModel(returnType, returnVal);
                var exitModel = new InterceptionExitModel(targetTypeVal, returnTypeVal, input.MethodBase);
                engine.OnExit(exitModel);
            }

            return result;
        }
    }
}
