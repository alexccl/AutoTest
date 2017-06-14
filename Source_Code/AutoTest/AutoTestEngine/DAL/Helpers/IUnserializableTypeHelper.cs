using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Helpers
{
    /// <summary>
    /// Helper for recognizing and storing types that are unserializable
    /// </summary>
    internal interface IUnserializableTypeHelper
    {
        /// <summary>
        /// Checks the known unserializable types to determine if type is unserializable
        /// </summary>
        /// <param name="t">The type to check if serializable</param>
        /// <returns>True if the type is unserializable</returns>
        bool IsUnserializable(Type t);

        /// <summary>
        /// Adds the unserializable type to persistant storage
        /// </summary>
        /// <param name="t">The type to add to storage</param>
        void AddUnserializableType(Type t);
    }
}
