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
    class InstanceSerializationVerifier : IVerifier
    {
        private ISerializationHelper _serializer;
        private IUnserializableTypeHelper _helper;

        public InstanceSerializationVerifier(ISerializationHelper serializer, IUnserializableTypeHelper dalHelper)
        {
            _serializer = serializer;
            _helper = dalHelper;
        }
        public int VerificationPriority
        {
            get
            {
                return 2;
            }
        }

        public List<VerificationFailure> Verify(InterceptionProcessingData processingData)
        {
            var returnVal = new List<VerificationFailure>();
            var instanceType = processingData.TargetInstance.GetType();

            if (processingData.BoundaryType != BoundaryType.Entry)
                return returnVal;

            if(_helper.IsUnserializable(instanceType))
            {
                returnVal.Add(new TypeSerializationFailure(instanceType));
            }
            else
            {
                var serResult = _serializer.Serialize(processingData.TargetInstance);
                if (!serResult.Success)
                {
                    _helper.AddUnserializableType(instanceType);
                    returnVal.Add(new TypeSerializationFailure(instanceType));
                }
            }

            return returnVal;
        }
    }
}
