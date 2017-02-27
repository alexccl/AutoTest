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

        public static string GetFriendlyName(this Type type)
        {
            string friendlyName = type.Name;
            if (type.IsGenericType)
            {
                int iBacktick = friendlyName.IndexOf('`');
                if (iBacktick > 0)
                {
                    friendlyName = friendlyName.Remove(iBacktick);
                }
                friendlyName += "<";
                Type[] typeParameters = type.GetGenericArguments();
                for (int i = 0; i < typeParameters.Length; ++i)
                {
                    string typeParamName = typeParameters[i].Name;
                    friendlyName += (i == 0 ? typeParamName : "," + typeParamName);
                }
                friendlyName += ">";
            }
            else if (type.Equals(typeof(void)))
            {
                friendlyName = "void";
            }

            return friendlyName;
        }
    }
}
