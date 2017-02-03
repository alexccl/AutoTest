using AutoTestEngine.ProcessMultiplexer.Processes.ExecutionRecorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.Helpers
{
    internal static class ExtensionMethods
    {
        public static List<TypeValModel> ToTypeValList(this List<Object> objList)
        {
            var returnVal = new List<TypeValModel>();

            if (objList == null || objList.Count == 0) return returnVal;

            foreach(var obj in objList) returnVal.Add(new TypeValModel(obj.GetType(), obj));

            return returnVal;
        }
    }
}
