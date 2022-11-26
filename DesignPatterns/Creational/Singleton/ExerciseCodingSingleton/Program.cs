namespace ExerciseCodingSingleton
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        // Singleton Coding Exercise
        // Since implementing a singleton is easy, you have a different challenge: write a method called IsSingleton()
        // This method takes a factory method that returns an object and it's up to you to determine whether or not that object is a singleton instance.

        public class SingletonTester
        {
            public static bool IsSingleton(Func<object> func)
            {
                //return func.Invoke().GetHashCode().Equals(func.Invoke().GetHashCode());
                
                // OR
                
                return func.Invoke().Equals(func.Invoke());
            }
        }
    }
}