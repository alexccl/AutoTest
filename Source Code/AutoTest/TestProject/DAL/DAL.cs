using AutoTestEngine.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DAL
{
    public class DAL : IDAL
    {
        [Dependency]
        private IRepository _repository;
        public DAL(IRepository repository)
        {
            _repository = repository;
        }

        public void Create<T>(T createdObject)
        {
            _repository.GetTypeRepository<T>().Add(createdObject);
        }

        public void Delete<T>(Predicate<T> predicate)
        {
            _repository.GetTypeRepository<T>().RemoveAll(predicate);
        }

        public List<T> Read<T>(Expression<Func<T, bool>> predicate)
        {
            return _repository.GetTypeRepository<T>().AsQueryable().Where<T>(predicate).ToList();
        }
    }
}
