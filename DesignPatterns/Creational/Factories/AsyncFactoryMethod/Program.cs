//using System.Threading.Tasks;
using static System.Console;

//var foo = new Foo();
//await foo.InitAsync(); // you have to not forget
WriteLine("creating foo");
var fooObject = await Foo.CreateAsync();
WriteLine("foo created");

public class Foo
{
    //public Foo()
    //{
    //	await Task.Delay(1000);
    //	//you cannot await in a construstor
    //}

    private Foo() { }

    /*public*/
    private async Task<Foo> InitAsync()
	{
		await Task.Delay(1000);
		return this;
	}

	public static Task<Foo> CreateAsync()
	{
		var resuls = new Foo();
		return resuls.InitAsync();
	}
}