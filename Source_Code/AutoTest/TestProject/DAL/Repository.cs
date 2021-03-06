﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DAL
{
    public class Repository : IRepository
    {
        private Dictionary<Type, object> _dict = new Dictionary<Type, object>();
        public Repository()
        {
            _dict.Add(typeof(long), new List<long>() { 3});
        }
        public List<T> GetTypeRepository<T>()
        {
            var repositoryKeyType = typeof(T);
            if (!_dict.ContainsKey(repositoryKeyType)) _dict.Add(repositoryKeyType, new List<T>());

            return (List<T>)_dict[repositoryKeyType];
        }
    }
}
