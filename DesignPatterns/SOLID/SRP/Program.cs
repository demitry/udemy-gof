using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace SRP
{
    class Program
    {

        public class Journal
        {
            private readonly List<String> entries = new List<String>();

            private static int count = 0;

            public int AddEntry(String text)
            {
                entries.Add($"{++count}: {text}");
                return count; //memento
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }

            // DON'T DO LIKE THIS, use Persistence class
            /*
            public void Save(string filename)
            {
                File.WriteAllText(filename, ToString());
            }

            public static Journal Load(string filename)
            {

            }

            public void Load(Uri uri)
            {

            }
            */
        }

        public class Persistence
        {
            public void SaveToFile(Journal journal, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                {
                    File.WriteAllText(filename, journal.ToString());
                }
            }
        }

        static void Main(string[] args)
        {
            var journal = new Journal();
            journal.AddEntry("I cried today");
            journal.AddEntry("I ate a bug");
            WriteLine(journal);
            Console.WriteLine("Hello World!");

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(journal, filename, true);
        }
    }
}
