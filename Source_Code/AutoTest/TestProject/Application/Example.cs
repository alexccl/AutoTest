using AutoTestEngine.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestProject.DAL;

namespace TestProject.Application
{
public class Example : IExample
{
    //database layer accessor
    [Dependency]
    private IDAL _dataAccessor;

    public Example(IDAL dataAccessor)
    {
        _dataAccessor = dataAccessor;
    }

    public long AddNumberToDBVal(long number)
    {
        //retrieve the database value
        var databaseValue = _dataAccessor.ReadAll<long>().First();

        return number + databaseValue;
    }
}
}
