using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL.TexFileImplementation
{
    internal class TextFileDAL : IDAL
    {
        private IRepository _repository;
        public TextFileDAL(IRepository repository)
        {
            _repository = repository;
        }

        public void CommitChanges()
        {
            _repository.CommitChanges();
        }

        public void Create<T>(T entity)
        {
            var rep = _repository.GetTypeRepostiory<T>();
            rep.Add(entity);
            _repository.SetTypeRepository<T>(rep);
        }

        public List<T> Fetch<T>(Func<T, bool> predicate) where T : class
        {
            var rep = _repository.GetTypeRepostiory<T>();
            return rep.Where(predicate).ToList();
        }

        public void Remove<T>(T entity) where T : class
        {
            var rep = _repository.GetTypeRepostiory<T>();
            rep.Remove(entity);
            _repository.SetTypeRepository<T>(rep);
        }
    }
}
