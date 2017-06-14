using AutoTestEngine.TestGeneration.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTestEngine.TestGeneration
{
    internal class Generator
    {
        public void Generate(TestData data)
        {
            var template = new UnitTestGenerator();
            template.Session = new Dictionary<string, object>();
            template.Session.Add("data", data);
            template.Initialize();
            var code = template.TransformText();
            OutputUnitTest(code);
        }

        private void OutputUnitTest(string contents)
        {
            var path = @"C:\AutoTest\Tests.cs";
            File.WriteAllText(path, contents);
        }
    }
}
