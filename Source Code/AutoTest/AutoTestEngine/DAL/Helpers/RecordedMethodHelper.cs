using AutoTestEngine.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Helpers
{
    internal class RecordedMethodHelper : IRecordedMethodHelper
    {
        private IDAL _dal;

        public RecordedMethodHelper(IDAL dal)
        {
            _dal = dal;
        }

        public void AddRecordedMethod(RecordedMethod method) { }

        public List<RecordedMethod> GetAllRecordedMethods()
        {
            throw new NotImplementedException();
        }

        public RecordedMethod GetMethodWithId()
        {
            throw new NotImplementedException();
        }
    }
}
