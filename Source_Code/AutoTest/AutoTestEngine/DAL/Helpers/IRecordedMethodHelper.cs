using System.Collections.Generic;
using AutoTestEngine.DAL.Models;
using System;

namespace AutoTestEngine.DAL.Helpers
{
    /// <summary>
    /// Helps manage and organize the methods that have been recorded
    /// </summary>
    internal interface IRecordedMethodHelper
    {
        /// <summary>
        /// Adds recorded method to persistent storage
        /// </summary>
        /// <param name="method">the method to be stored</param>
        void AddRecordedMethod(RecordedMethod method);

        /// <summary>
        /// Gets all recorded methods in persistent storage
        /// </summary>
        /// <returns>All recorded methods</returns>
        List<RecordedMethod> GetAllRecordedMethods();

        /// <summary>
        /// Gets recorded method with specific identifier
        /// </summary>
        /// <param name="id">identifier to filter on</param>
        /// <returns>The method with specified ID.  Is null if method not found</returns>
        RecordedMethod GetMethodWithId(Guid id);
    }
}