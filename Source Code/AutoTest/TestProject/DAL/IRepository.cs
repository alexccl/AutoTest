using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.DAL
{
    public interface IRepository
    {
        List<T> GetTypeRepository<T>();
    }
}
