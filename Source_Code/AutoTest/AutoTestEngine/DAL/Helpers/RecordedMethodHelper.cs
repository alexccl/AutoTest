using AutoTestEngine.DAL.Models;
using AutoTestEngine.DAL.TexFileImplementation;
using AutoTestEngine.Helpers.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Helpers
{
    [Serializable]
    internal class RecordedMethodHelper : IRecordedMethodHelper
    {
        private IDAL _dal;

        public RecordedMethodHelper(IDAL dal)
        {
            _dal = dal;
        }

        private RecordedMethod CloneRecordedMethod(RecordedMethod source)
        {
            var serializer = new SerializationHelper();
            var serVal = serializer.Serialize(source).SerializedValue.Value;

            return serializer.Deserialize<RecordedMethod>(serVal);
        }

        public void AddRecordedMethod(RecordedMethod method)
        {
            var existingMethod = GetMethodWithId(method.Identifier);

            if (existingMethod != null) _dal.Remove<RecordedMethod>(existingMethod);

            _dal.Create<RecordedMethod>(CloneRecordedMethod(method));
            _dal.CommitChanges();
        }

        public List<RecordedMethod> GetAllRecordedMethods()
        {
            return _dal.Fetch<RecordedMethod>(x => true);
        }

        public RecordedMethod GetMethodWithId(Guid id)
        {
            return _dal.Fetch<RecordedMethod>(x => x.Identifier.Equals(id)).FirstOrDefault();
        }
    }
}
