using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DAL
{
    public interface IDAL
    {
        void Create<T>(T createdObject);
        List<T> Read<T>(Expression<Func<T, bool>> predicate);
        void Delete<T>(Predicate<T> predicate);
    }
}
