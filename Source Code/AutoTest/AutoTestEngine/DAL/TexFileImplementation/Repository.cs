﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTestEngine.Helpers.Serialization;
using AutoTestEngine.Helpers;

namespace AutoTestEngine.DAL.TexFileImplementation
{
    public class Repository : IRepository
    {
        private static Dictionary<Type, List<object>> _storedObjectBacking
        {
            get;
            set;
        }
        public static Dictionary<Type, List<object>> StoredObject
        {
            get
            {
                if (_storedObjectBacking == null) _storedObjectBacking = new Dictionary<Type, List<object>>();
                return _storedObjectBacking;
            }
            set
            {
                _storedObjectBacking = value;
            }
        }
        private readonly string _storageFilePath = @"C:\Storage.txt";
        private ISerializationHelper _serializationHelper;

        public List<T> GetTypeRepostiory<T>()
        {
            if (!StoredObject.ContainsKey(typeof(T))) StoredObject.Add(typeof(T), new List<object>());

            //if it's empty, it may need to sync up with storage
            if(!StoredObject[typeof(T)].Any())
            {
                StoredObject[typeof(T)] = RetrieveStoredContents<T>().Select(x => (object)x).ToList();
            }

            return StoredObject[typeof(T)].Select(x => (T)((object)x).DeepClone()).ToList();
        }

        public void SetTypeRepository<T>(List<T> newRepository)
        {
            if (StoredObject.ContainsKey(typeof(T)))
                StoredObject[typeof(T)] = newRepository.Select(x => ((object)x).DeepClone()).ToList();
            else
                StoredObject.Add(typeof(T), newRepository.Select(x => ((object)x).DeepClone()).ToList());
        }

        public Repository(ISerializationHelper serializationHelper)
        {
            _serializationHelper = serializationHelper;
            
        }

        public void CommitChanges()
        {
            WriteToStorage();
        }

        private List<T> RetrieveStoredContents<T>()
        {
            EnsureFileExistence();
            var contents = File.ReadAllText(_storageFilePath);
            var serVals = _serializationHelper.Deserialize<Dictionary<Type, string>>(contents) ?? new Dictionary<Type, string>();

            if (!serVals.ContainsKey(typeof(T))) return new List<T>();

            var stringVal = serVals[typeof(T)];
            var listVal = _serializationHelper.Deserialize<List<object>>(stringVal)
                                              .Select(x => (T)x)
                                              .ToList();
            return listVal;

        }

        private void WriteToStorage()
        {
            EnsureFileExistence();
            var serVals = new Dictionary<Type, string>();
            foreach(var entry in StoredObject)
            {
                var serVal = _serializationHelper.Serialize(entry.Value);
                serVals.Add(entry.Key, serVal.SerializedValue.Value);
            }

            var newContents = _serializationHelper.Serialize(serVals);
            if (!newContents.Success) throw new Exception("Could not serialize data for repository", newContents.FailureException);

            File.WriteAllText(_storageFilePath, newContents.SerializedValue.Value);
        }

        private void EnsureFileExistence()
        {
            if (!File.Exists(_storageFilePath)) File.Create(_storageFilePath);
        }
    }
}
