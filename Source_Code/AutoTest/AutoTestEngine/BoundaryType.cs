using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// Enumeration of the different boundaries the interception occured on
    /// </summary>
    enum BoundaryType
    {
        /// <summary>
        /// Before calling the proxied object
        /// </summary>
        Entry,
        /// <summary>
        /// The exception thrown by the proxied object
        /// </summary>
        Exception,
        /// <summary>
        /// After calling the proxied object
        /// </summary>
        Exit
    }
}
