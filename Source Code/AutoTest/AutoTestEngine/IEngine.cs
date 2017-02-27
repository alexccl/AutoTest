using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine
{
    /// <summary>
    /// Entry point for interception processing
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// Method to be called between proxy and proxied class
        /// </summary>
        EntryProcessingResult OnEntry(InterceptionEntryModel entryModel);

        /// <summary>
        /// Method to be called on proxied class exception
        /// </summary>
        void OnException(InterceptionExceptionModel exceptionModel);

        /// <summary>
        /// Method to be called after proxied class/method invocation and return to caller
        /// </summary>
        void OnExit(InterceptionExitModel exitModel);

        /// <summary>
        /// Generates new suite of tests with the stored execution recordings
        /// </summary>
        void GenerateTests();
    }
}
