using System.Collections.Generic;
using AutoTestEngine.DAL.Models;
using System;

namespace AutoTestEngine.DAL.Helpers
{
    internal interface IRecordedMethodHelper
    {
        void AddRecordedMethod(RecordedMethod method);
        List<RecordedMethod> GetAllRecordedMethods();
        RecordedMethod GetMethodWithId(Guid id);
    }
}