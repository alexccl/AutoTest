using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.DAL
{
    internal interface IDAL
    {
        void Create<T>(T entity);
        List<T> Fetch<T>(Func<T, bool> predicate) where T : class;
        void Remove<T>(T entity) where T : class;
        void CommitChanges();
    }
}
