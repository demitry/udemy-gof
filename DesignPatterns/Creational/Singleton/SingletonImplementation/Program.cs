using DesignPatterns;
using MoreLinq;
using NUnit.Framework;

var db = SingletonDatabase.Instance;
var city = "Tokyo";
Console.WriteLine($"{city} has population {db.GetPopulation(city)}");

namespace DesignPatterns
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount = 0;
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;
            Console.WriteLine("Initializing database");

            var directory = new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName;
            var dbFilePath = Path.Combine(directory, "capitals.txt");

            capitals = File.ReadAllLines(dbFilePath)
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1).Trim())
                );
        }

        public int GetPopulation(string name)
        {
            return capitals[name];
        }

        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (string name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }

            return result;
        }
    }

    [TestFixture]
    public class SingletonTests
    {
        [Test]
        public void IsSingletonTest() 
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Assert.That(db, Is.SameAs(db2));
            Assert.That(SingletonDatabase.Count, Is.EqualTo(1));
        }

        [Test]
        public void SingletonTotalPopulationTest()
        {
            var recordFinder = new SingletonRecordFinder();
            var names = new[] { "Tokyo", "New York", "San Paulo" };
            int totalCount = recordFinder.GetTotalPopulation(names);
            Assert.That(totalCount, Is.EqualTo(33200000 + 17800000 + 17700000));
        }

        // Problem: We dependent on live database!
        // RecordFinder has a hard-coded reference to the singleton instance!
    }
}