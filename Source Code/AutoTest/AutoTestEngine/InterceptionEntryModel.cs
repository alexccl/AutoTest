using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class InterceptionEntryModel
    {
        public object TargetInstance { get; set; }
        public List<object> MethodArgs { get; set; }
        public MethodBase Method { get; set; }
    }
}
