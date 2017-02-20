using AutoTestEngine.DAL;
using AutoTestEngine.DAL.Helpers;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.InterceptionVerification.VerificationResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.InterceptionVerification.Verifiers
{

    internal class InputParameterVerifier : IVerifier
    {
        private ISerializationHelper _serializer;
        private IUnserializableTypeHelper _dalHelper;

        public InputParameterVerifier(ISerializationHelper serializer, IUnserializableTypeHelper dalHelper)
        {
            _serializer = serializer;
            _dalHelper = dalHelper;
        }
        public int VerificationPriority
        {
            get
            {
                return 1;
            }
        }

        public List<VerificationFailure> Verify(InterceptionProcessingData processingData)
        {
            var res = new List<VerificationFailure>();
            if (processingData.BoundaryType != BoundaryType.Entry) return res;

            foreach(var arg in processingData.MethodArgs)
            {
                res.AddRange(ProcessArg(arg));
            }

            return res;
        }

        private List<VerificationFailure> ProcessArg(TypeValModel arg)
        {
            var res = new List<VerificationFailure>();
            var argType = arg.GetType();
            if (_dalHelper.IsUnserializable(argType))
            {
                res.Add(new TypeSerializationFailure(argType));
            }
            else
            {;
                var serResult = _serializer.Serialize(arg);
                if (!serResult.Success)
                {
                    _dalHelper.AddUnserializableType(argType);
                    res.Add(new TypeSerializationFailure(argType));
                }
            }

            return res;
        }
    }
}
