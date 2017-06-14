using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// Result of entry interception
    /// </summary>
    public class EntryProcessingResult
    {
        /// <summary>
        /// Determines whether or not the proxy should bypass the proxied class invocation
        /// </summary>
        public bool BypassProxiedMethod { get; set; }

        /// <summary>
        /// Determines the return value of the bapassed method
        /// </summary>
        public object BypassProxiedMethodValue { get; set; }
    }
}
