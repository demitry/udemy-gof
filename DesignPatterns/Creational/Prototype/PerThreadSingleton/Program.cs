Console.WriteLine("Need one sigleton per thread? " +
	"We Can use DI, but also can do it manually.");

//1. Lazy<> gives thread safety.
//2. [ThreadStatic] ThreadLocal<> - provides thread-local storage of data.

var t1 = Task.Factory.StartNew(() => 
{
	Console.WriteLine($"t1: {PerThreadSingleton.Instance.Id}");
});

var t2 = Task.Factory.StartNew(() =>
{
    Console.WriteLine($"t2: {PerThreadSingleton.Instance.Id}");
    Console.WriteLine($"t2: {PerThreadSingleton.Instance.Id}");
});

Task.WaitAll(t1, t2);


public sealed class PerThreadSingleton
{
	private static ThreadLocal<PerThreadSingleton> threadInstance 
		= new ThreadLocal<PerThreadSingleton>(
			() => new PerThreadSingleton() );

	public int Id;

	private PerThreadSingleton()
	{
        //Id = Thread.CurrentThread.ManagedThreadId; 
		// CA1840: Use Environment.CurrentManagedThreadId instead of Thread.CurrentThread.ManagedThreadId
        
		Id = Environment.CurrentManagedThreadId;
	}

	public static PerThreadSingleton Instance => threadInstance.Value;
}

