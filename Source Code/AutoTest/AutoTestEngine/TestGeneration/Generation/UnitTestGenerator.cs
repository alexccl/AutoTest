﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 14.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace AutoTestEngine.TestGeneration.Generation
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using AutoTestEngine.Helpers;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public partial class UnitTestGenerator : UnitTestGeneratorBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System.Reflection;\r\nusing Newtonsoft.Json;\r\nusing Moq;\r\nusing KellermanSoft" +
                    "ware.CompareNetObjects;\r\n\r\n\t[TestClass]\r\n\tpublic class AutoTest_Generated_Tests\r" +
                    "\n\t{\r\n");
            
            #line 16 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
 foreach(var test in data.Tests) 
{
            
            #line default
            #line hidden
            this.Write("\t\t[TestMethod]\r\n\t\tpublic void ");
            
            #line 19 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.TestName));
            
            #line default
            #line hidden
            this.Write("()\r\n\t\t{\r\n\t\t\tvar instance = (");
            
            #line 21 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.InstanceType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")DeserializeObject(typeof(");
            
            #line 21 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.InstanceType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write("), \"");
            
            #line 21 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.ObjectInstance.Replace("\"", "\\\"")));
            
            #line default
            #line hidden
            this.Write("\");\r\n\r\n");
            
            #line 23 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		foreach(var dep in test.Dependencies)
		{
		var mockVarName = "mock_" + dep.MemberType.GetFriendlyName();
		
            
            #line default
            #line hidden
            this.Write("\t\t\tvar ");
            
            #line 27 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(mockVarName));
            
            #line default
            #line hidden
            this.Write(" = new Mock<");
            
            #line 27 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dep.MemberType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(">();\r\n");
            
            #line 28 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
			foreach(var method in dep.Methods)
			{
		
            
            #line default
            #line hidden
            
            #line 31 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

				// mock param setup
				var variableList = "";
				if(method.MethodArgs.Count > 0)
				{
					variableList = "It.IsAny<" + method.MethodArgs[0].GetFriendlyName() + ">()";
				}
				for(int i = 1; i < method.MethodArgs.Count; i++){
					variableList += ", It.IsAny<" + method.MethodArgs[i].GetFriendlyName() + ">()";
				}

            
            #line default
            #line hidden
            this.Write("\t\t\t");
            
            #line 42 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(mockVarName));
            
            #line default
            #line hidden
            this.Write(".Setup(x => x.");
            
            #line 42 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(method.MethodName));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 42 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(variableList));
            
            #line default
            #line hidden
            this.Write("))");
            
            #line 42 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

				foreach(var retVal in method.MethodCallReturs)
				{
					if(retVal.ExceptionThrown)
					{    
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t.Throws((");
            
            #line 48 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(retVal.ExceptionType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")DeserializeObject(typeof(");
            
            #line 48 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(retVal.ExceptionType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write("), \"");
            
            #line 48 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(retVal.SerializedException.Replace("\"", "\\\"")));
            
            #line default
            #line hidden
            this.Write("\"))");
            
            #line 48 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

					}
					else{
            
            #line default
            #line hidden
            this.Write("\t\t\t\t\t\t\r\n\t\t\t\t\t\t\t.Returns((");
            
            #line 52 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(retVal.ReturnVal.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")DeserializeObject(typeof(");
            
            #line 52 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(retVal.ReturnVal.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write("), \"");
            
            #line 52 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(retVal.SerializedValue.Replace("\"", "\\\"")));
            
            #line default
            #line hidden
            this.Write("\"))");
            
            #line 52 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

					}
				}
            
            #line default
            #line hidden
            this.Write(";\r\n\r\n\t\t\tinstance = (");
            
            #line 56 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.InstanceType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")SetPropertyOnType(typeof(");
            
            #line 56 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.InstanceType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write("), instance, typeof(");
            
            #line 56 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(dep.MemberType.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write("), ");
            
            #line 56 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(mockVarName));
            
            #line default
            #line hidden
            this.Write(".Object);\r\n\r\n");
            
            #line 58 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
			}
            
            #line default
            #line hidden
            
            #line 59 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		}
            
            #line default
            #line hidden
            
            #line 60 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		foreach(var arg in test.Args)
		{
            
            #line default
            #line hidden
            this.Write("\t\t\t");
            
            #line 62 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(arg.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(" ");
            
            #line 62 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(arg.GeneratedArgName));
            
            #line default
            #line hidden
            this.Write(" = (");
            
            #line 62 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(arg.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")DeserializeObject(");
            
            #line 62 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(arg.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(",\"");
            
            #line 62 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(arg.SerializedArgInstance.Replace("\"", "\\\"")));
            
            #line default
            #line hidden
            this.Write("\");\r\n");
            
            #line 63 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		}
		if(test.WasExceptionThrown)
		{
            
            #line default
            #line hidden
            this.Write("\t\t\tvar thrownException = null;\r\n\t\t\ttry\r\n\t\t\t{\r\n");
            
            #line 69 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
			if(test.Args.Count == 0)
			{

            
            #line default
            #line hidden
            this.Write("\t\t\t\tvar testResult = instance.");
            
            #line 71 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.MethodName));
            
            #line default
            #line hidden
            this.Write("();\r\n\t\t\t");
            
            #line 72 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
}
			else{

            
            #line default
            #line hidden
            this.Write("\t\t\t\tvar testResult = instance.");
            
            #line 74 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.MethodName));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 74 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.Args[0].GeneratedArgName));
            
            #line default
            #line hidden
            
            #line 74 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

				for(int i = 1; i < test.Args.Count; i++)
			    {
					
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 77 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.Args[i].GeneratedArgName));
            
            #line default
            #line hidden
            
            #line 77 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

			    }
            
            #line default
            #line hidden
            this.Write(");");
            
            #line 78 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

			}
            
            #line default
            #line hidden
            this.Write("\t\t\t}\r\n\t\t\tcatch(Exception ex)\r\n\t\t\t{\r\n\t\t\t\tthrownException = ex;\r\n\t\t\t}\r\n");
            
            #line 85 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		}
		else
		{
			if(test.Args.Count == 0)
			{

            
            #line default
            #line hidden
            this.Write("\t\t\tvar testResult = instance.");
            
            #line 90 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.MethodName));
            
            #line default
            #line hidden
            this.Write("();\r\n");
            
            #line 91 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
			}
			else
			{

            
            #line default
            #line hidden
            this.Write("\t\t\tvar testResult = instance.");
            
            #line 94 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.MethodName));
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 94 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.Args[0].GeneratedArgName));
            
            #line default
            #line hidden
            
            #line 94 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

				for(int i = 1; i < test.Args.Count; i++)
			    {
					
            
            #line default
            #line hidden
            this.Write(", ");
            
            #line 97 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.Args[i].GeneratedArgName));
            
            #line default
            #line hidden
            
            #line 97 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

			    }
            
            #line default
            #line hidden
            this.Write(");");
            
            #line 98 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

			}
        }
		if(test.WasExceptionThrown)
		{
            
            #line default
            #line hidden
            this.Write("\t\t\tAssert.IsTrue(thrownException != null,\r\n\t\t\t\t\t\t");
            
            #line 104 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.TestName));
            
            #line default
            #line hidden
            this.Write(" + \" failed to throw a \" + ");
            
            #line 104 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.ThrownException.GetType().GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(" + \" exception.\");\r\n\r\n\t\t\tAssert.IsTrue(thrownException.GetType().Equals(typeof(");
            
            #line 106 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.ThrownException.GetType().GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")),\r\n\t\t\t\t\t\t");
            
            #line 107 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.TestName));
            
            #line default
            #line hidden
            this.Write(" + \" was expected to throw an exception of type \" + ");
            
            #line 107 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.ThrownException.GetType().GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(" + \" but instead through an exception of type \" + thrownException.GetType().GetFr" +
                    "iendlyName());\r\n");
            
            #line 108 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		}
		else
		{
            
            #line default
            #line hidden
            this.Write("\r\n\r\n\t\t\tvar expectedReturnVal = (");
            
            #line 113 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.ReturnVal.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write(")DeserializeObject(typeof(");
            
            #line 113 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.ReturnVal.Type.GetFriendlyName()));
            
            #line default
            #line hidden
            this.Write("), \"");
            
            #line 113 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.SerializedReturnVal.Replace("\"", "\\\"")));
            
            #line default
            #line hidden
            this.Write("\");\r\n\t\t\tvar equalityResult = Compare(testResult, expectedReturnVal);\r\n\t\t\tAssert.I" +
                    "sTrue(equalityResult.AreEqual, \r\n\t\t\t\t\t\t");
            
            #line 116 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(test.TestName));
            
            #line default
            #line hidden
            this.Write(" + \" failed testing equality with the message: \" + res.DifferencesString);\r\n");
            
            #line 117 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
		}
            
            #line default
            #line hidden
            this.Write("\t}\r\n");
            
            #line 119 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"
}
            
            #line default
            #line hidden
            this.Write(@"	
		private string ComparisonResult Compare(object obj1, object obj2)
		{
			CompareLogic compareLogic = new CompareLogic();
	        return compareLogic.Compare()
		}
	
		private object SetPropertyOnType(Type classType, object classInstance, Type propertyType, object propertyInstance)
		{
			var props = classType.GetProperties();
			foreach(var prop in props)
			{
				if(prop.PropertyType != propertyType) continue;
	
				prop.SetValue(classInstance, propertyInstance);
				return classInstance;
			}
	
			throw new Exception(""Could not find type: "" + propertyType.Name + "" on type: "" + classType.Name);
		}
		
		private object DeserializeObject(Type t, string obj)
		{
			var method = typeof(JsonConvert).GetMethod(""DeserializeObject"").MakeGenericMethod(t);
			return method.Invoke(null, obj);
		}
	}");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 1 "C:\Users\Alex.Luebbehusen\Documents\Visual Studio 2015\Projects\at-gh\Source Code\AutoTest\AutoTestEngine\TestGeneration\Generation\UnitTestGenerator.tt"

private global::AutoTestEngine.TestGeneration.TestData _dataField;

/// <summary>
/// Access the data parameter of the template.
/// </summary>
private global::AutoTestEngine.TestGeneration.TestData data
{
    get
    {
        return this._dataField;
    }
}


/// <summary>
/// Initialize the template
/// </summary>
public virtual void Initialize()
{
    if ((this.Errors.HasErrors == false))
    {
bool dataValueAcquired = false;
if (this.Session.ContainsKey("data"))
{
    this._dataField = ((global::AutoTestEngine.TestGeneration.TestData)(this.Session["data"]));
    dataValueAcquired = true;
}
if ((dataValueAcquired == false))
{
    object data = global::System.Runtime.Remoting.Messaging.CallContext.LogicalGetData("data");
    if ((data != null))
    {
        this._dataField = ((global::AutoTestEngine.TestGeneration.TestData)(data));
    }
}


    }
}


        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "14.0.0.0")]
    public class UnitTestGeneratorBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
