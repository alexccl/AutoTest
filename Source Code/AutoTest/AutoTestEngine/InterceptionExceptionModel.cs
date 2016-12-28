using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class InterceptionExceptionModel
    {
        public Exception Exception { get; set; }
        public object TargetInstance { get; set; }
    }
}
