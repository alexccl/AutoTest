using AutoTestEngine.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    internal class TestData
    {
        public List<SingleTest> Tests { get; set; }
        internal TestData(List<RecordedMethod> methods)
        {
            this.Tests = methods
                         .Where(x => x.ReturnVal != null) //only write tests for recorded methods that have return vals
                         .Select(x => new SingleTest(x)).ToList();
        }


    }
}
