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
        private readonly string _storageFilePath = @"Storage.txt";

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

        public Repository()
        {
            RetrieveStoredContents();
        }

        public void CommitChanges()
        {
            WriteToStorage();
        }

        private void RetrieveStoredContents()
        {
            var contents = File.ReadAllText(_storageFilePath);
            this.StoredObject = SerializationHelper.Deserialize<Dictionary<Type, List<object>>>(contents);
        }

        private void WriteToStorage()
        {
            var newContents = SerializationHelper.Serialize(this.StoredObject);
            File.WriteAllText(_storageFilePath, newContents);
        }
    }
}
