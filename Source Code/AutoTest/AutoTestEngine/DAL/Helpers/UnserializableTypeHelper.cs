using AutoTestEngine.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.Helpers
{
    internal class UnserializableTypeHelper : IUnserializableTypeHelper
    {
        private IDAL _dal;
        public UnserializableTypeHelper(IDAL dal)
        {
            _dal = dal;
        }
        public void AddUnserializableType(Type t)
        {
            if (!IsUnserializable(t))
                _dal.Create<UnserializableType>(new UnserializableType(t));
        }

        public bool IsUnserializable(Type t)
        {
            return _dal.Fetch<UnserializableType>(x => x.InvalidType.Equals(t)).Any();
        }
    }
}
