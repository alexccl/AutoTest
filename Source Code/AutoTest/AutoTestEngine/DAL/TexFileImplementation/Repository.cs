using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoTestEngine.Helpers.Serialization;

namespace AutoTestEngine.DAL.TexFileImplementation
{
    internal class Repository : IRepository
    {

        public Dictionary<Type, List<object>> StoredObject {get;set;}
        private readonly string _storageFilePath = @"C:\Storage.txt";
        private ISerializationHelper _serializationHelper;

        public List<T> GetTypeRepostiory<T>()
        {
            if (StoredObject.ContainsKey(typeof(T)))
            {
                var typeRep = StoredObject[typeof(T)];
                return typeRep.Select(x => (T)x).ToList();
            }
            else
            {
                return new List<T>();
            }
        }

        public void SetTypeRepository<T>(List<T> newRepository)
        {
            if (StoredObject.ContainsKey(typeof(T)))
                StoredObject[typeof(T)] = newRepository.Select(x => (object)x).ToList();
            else
                StoredObject.Add(typeof(T), newRepository.Select(x => (object)x).ToList());
        }

        public Repository(ISerializationHelper serializationHelper)
        {
            _serializationHelper = serializationHelper;
            RetrieveStoredContents();
        }

        public void CommitChanges()
        {
            WriteToStorage();
        }

        private void RetrieveStoredContents()
        {
            EnsureFileExistence();
            var contents = File.ReadAllText(_storageFilePath);
            this.StoredObject = _serializationHelper.Deserialize<Dictionary<Type, List<object>>>(contents) ?? new Dictionary<Type, List<object>>();
        }

        private void WriteToStorage()
        {
            EnsureFileExistence();
            var newContents = _serializationHelper.Serialize(this.StoredObject, typeof(Dictionary<Type, List<object>>));
            if (!newContents.Success) throw new Exception("Could not serialize data for repository", newContents.FailureException);

            File.WriteAllText(_storageFilePath, newContents.Result);
        }

        private void EnsureFileExistence()
        {
            if (!File.Exists(_storageFilePath)) File.Create(_storageFilePath);
        }
    }
}
