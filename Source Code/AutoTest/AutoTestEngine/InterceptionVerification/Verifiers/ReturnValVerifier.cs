using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTestEngine.InterceptionVerification.VerificationResult;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.DAL.Helpers;

namespace AutoTestEngine.InterceptionVerification.Verifiers
{
    internal class ReturnValVerifier : IVerifier
    {
        private ISerializationHelper _serializer;
        private IUnserializableTypeHelper _helper;

        public ReturnValVerifier(ISerializationHelper serializer, IUnserializableTypeHelper dalHelper)
        {
            _serializer = serializer;
            _helper = dalHelper;
        }

        public int VerificationPriority
        {
            get
            {
                return 3;
            }
        }

        public List<VerificationFailure> Verify(InterceptionProcessingData processingData)
        {
            var returnVal = new List<VerificationFailure>();
            if (processingData.BoundaryType != BoundaryType.Exit) return returnVal;

            if (_helper.IsUnserializable(processingData.ReturnType))
            {
                returnVal.Add(new TypeSerializationFailure(processingData.ReturnType));
            }
            else
            {
                var res = _serializer.Serialize(processingData.ReturnValue, processingData.ReturnType);
                if (!res.Success)
                {
                    _helper.AddUnserializableType(processingData.ReturnType);
                    returnVal.Add(new TypeSerializationFailure(processingData.ReturnType));
                }
            }

            return returnVal;
        }
    }
}
