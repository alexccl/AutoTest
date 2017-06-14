using System.Collections.Generic;

namespace AutoTestEngine.DAL.TexFileImplementation
{
    internal interface IRepository
    {
        void CommitChanges();
        List<T> GetTypeRepostiory<T>();
        void SetTypeRepository<T>(List<T> newRepository);
    }
}