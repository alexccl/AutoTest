using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    public class AutoTestEngineException : Exception
    {
        public AutoTestEngineException(string message) : base(message)
        {

        }
    }
}
