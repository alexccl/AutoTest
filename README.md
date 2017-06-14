# AutoTest

## What is it?
AutoTest is an experiment in automatically generating a regression test suite via capturing program execution metadata.

## Why would someone use it?
In theory, you could have this extension in a production environment.  Over the course of time, it could generate hundreds, thousands, or even millions of unique unit tests of (literally) real world test cases.  Then after a refactor, a developer could execute the test suite to ensure he did not make any breaking changes.  

## How do you use it?
Below is an example class that we will generate a regression suite for:
```
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

public interface IExample
{
    long AddNumberToDBVal(long number);
}
```

This is a very simple class that has a dependency on a database and contains one method that adds a number to the stored database value.  An important thing to notice is the "Dependency" attribute.  This will tell the AutoTest generator that method calls on this object need to be mocked out so that the unit test doesn't rely on actual database data.

The only other step to do is to register an interceptor to capture this data.  Technically many different interception or AOP libraries could be added to work with AutoTest, but as of now only a Unity plugin called "AutoTest4Unity" has been developed.  This plugin is registered below:

```
class Program
{
    static void Main(string[] args)
    {
        //register types and interceptors with the IOC container
        var container = new UnityContainer();
        container.AddNewExtension<Interception>();
        container.RegisterType<IDAL, DAL.DAL>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<AutoTestBehavior>());
        container.RegisterType<IRepository, Repository>(new Interceptor<InterfaceInterceptor>(), new InterceptionBehavior<AutoTestBehavior>());

        //resolve IOC dependencies and construct object
        var example = container.Resolve<IExample>();

        var result = example.AddNumberToDBVal(2);
        Console.WriteLine($"Adding 2 to the database value yields: {result}");
    }
}
```

That's all that has to be done!  Now everytime this code runs it will capture the metadata, store it, then generate C# unit tests.

## What do these unit tests look like?
Below is the unit test generated from executing the example above:
```
[TestMethod]
public void Example_AddNumberToDBVal_510777378()
{
		var instance = (Example)DeserializeObject(typeof(Example), "{\"$type\":\"TestProject.Application.Example, TestProject\"}");

		var mock_IDAL = new Mock<IDAL>();
		mock_IDAL.Setup(x => x.ReadAll<Int64>())						
						.Returns((List<Int64>)DeserializeObject(typeof(List<Int64>), "{\"$type\":\"System.Collections.Generic.List`1[[System.Int64, mscorlib]], mscorlib\",\"$values\":[3]}"));

		instance = (Example)SetPropertyOnType(typeof(Example), instance, typeof(IDAL), mock_IDAL.Object);

		Int64 arg_Int64_0 = (Int64)DeserializeObject(typeof(Int64),"2");
		var testResult = instance.AddNumberToDBVal(arg_Int64_0);

		var expectedReturnVal = (Int64)DeserializeObject(typeof(Int64), "5");
		var equalityResult = Compare(testResult, expectedReturnVal);
		Assert.IsTrue(equalityResult.AreEqual, 
					"Example_AddNumberToDBVal_510777378 failed testing equality with the message: " + equalityResult.DifferencesString);
}
```

There's a lot going on in this generated C# code, but breaking it down step by step:
1) Create an instance of the example class from the serialized value.
```
var instance = (Example)DeserializeObject(typeof(Example), "{\"$type\":\"TestProject.Application.Example, TestProject\"}");
```
2) Create a mock for the database, setting the return value for the method call _dataAccessor.ReadAll<long>()
```
var mock_IDAL = new Mock<IDAL>();
		mock_IDAL.Setup(x => x.ReadAll<Int64>())						
						.Returns((List<Int64>)DeserializeObject(typeof(List<Int64>), "{\"$type\":\"System.Collections.Generic.List`1[[System.Int64, mscorlib]], mscorlib\",\"$values\":[3]}"));
```
3) Replace the database object in the example class with our newly created mock object
```
instance = (Example)SetPropertyOnType(typeof(Example), instance, typeof(IDAL), mock_IDAL.Object);
```
4) Make a call to the method we are testing using the stored parameters.
```
Int64 arg_Int64_0 = (Int64)DeserializeObject(typeof(Int64),"2");
var testResult = instance.AddNumberToDBVal(arg_Int64_0);
```
5) Make an assertion by comparing the returned value with the value we stored during execution
```
var expectedReturnVal = (Int64)DeserializeObject(typeof(Int64), "5");
var equalityResult = Compare(testResult, expectedReturnVal);
Assert.IsTrue(equalityResult.AreEqual, 
					"Example_AddNumberToDBVal_510777378 failed testing equality with the message: " + equalityResult.DifferencesString);
```
