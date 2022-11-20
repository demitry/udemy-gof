using Autofac;
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

    public class DBUtils
    {
        public static string GetDbFileName()
        {
            string directory = string.Empty;
            try
            {
                directory = new FileInfo(typeof(IDatabase).Assembly.Location).DirectoryName ?? throw new NullReferenceException("Cannot find DB directory.");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }

            var dbFilePath = Path.Combine(directory, "capitals.txt");
            return dbFilePath;
        }
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount = 0;
        public static int Count => instanceCount;

        private SingletonDatabase()
        {
            instanceCount++;

            Console.WriteLine("Initializing Singleton database");
            
            string dbFilePath = DBUtils.GetDbFileName();

            capitals = File.ReadAllLines(dbFilePath)
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1).Trim())
                );
        }

        public int GetPopulation(string name) => capitals[name];

        private static Lazy<SingletonDatabase> instance =
            new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        public static SingletonDatabase Instance => instance.Value;
    }

    public class OrdinaryDatabase: IDatabase
    {
        private Dictionary<string, int> capitals;

        public OrdinaryDatabase()
        {
            Console.WriteLine("Initializing Ordinary database");

            string dbFilePath = DBUtils.GetDbFileName();

            capitals = File.ReadAllLines(dbFilePath)
                .Batch(2)
                .ToDictionary(
                    list => list.ElementAt(0).Trim(),
                    list => int.Parse(list.ElementAt(1).Trim())
                );
        }

        public int GetPopulation(string name) => capitals[name];
    }

    interface IRecordFinder
    {
        int GetTotalPopulation(IEnumerable<string> names);
    }

    public class SingletonRecordFinder : IRecordFinder
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

    public class ConfigurableRecordFinder : IRecordFinder
    {
        IDatabase database;

        public ConfigurableRecordFinder(IDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(paramName: nameof(database));
        }

        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (string name in names)
            {
                result += database.GetPopulation(name);
            }

            return result;
        }
    }

    public class DummyDatabase : IDatabase
    {
        public int GetPopulation(string name)
        {
            return new Dictionary<string, int>
            {
                ["alpha"] = 1,
                ["beta"] = 2,
                ["gamma"] = 3
            }[name];
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

        [Test]
        public void ConfigurablePopulationTest()
        {
            var recordFinder = new ConfigurableRecordFinder(new DummyDatabase());
            var names = new[] { "alpha", "gamma" };
            int totalCount = recordFinder.GetTotalPopulation(names);
            Assert.That(totalCount, Is.EqualTo(4));
        }

        [Test]
        public void DI_RealDB_PopulationTest()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<OrdinaryDatabase>() //Single point to change the DB to test
                .As<IDatabase>()
                .SingleInstance(); //Singleton

            containerBuilder.RegisterType<ConfigurableRecordFinder>();
            var names = new[] { "Tokyo", "New York", "San Paulo" };
            using (var container = containerBuilder.Build())
            {
                var recordFinder = container.Resolve<ConfigurableRecordFinder>();
                int totalCount = recordFinder.GetTotalPopulation(names);
                Assert.That(totalCount, Is.EqualTo(33200000 + 17800000 + 17700000));
            }
        }

        [Test]
        public void DI_DummyDB_PopulationTest()
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<DummyDatabase>() //Single point to change the DB to test
                .As<IDatabase>()
                .SingleInstance(); //Singleton

            containerBuilder.RegisterType<ConfigurableRecordFinder>();
            var names = new[] { "alpha", "gamma" };

            using (var container = containerBuilder.Build())
            {
                var recordFinder = container.Resolve<ConfigurableRecordFinder>();
                int totalCount = recordFinder.GetTotalPopulation(names);
                Assert.That(totalCount, Is.EqualTo(4));
            }
        }

    }
}