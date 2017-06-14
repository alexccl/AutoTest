using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// Encapsulates the configuration for the engine execution including if it is a unit test exectution, etc.
    /// </summary>
    public class EngineConfiguration
    {
        public bool IsUnitTesting { get; set; } 
    }
}
