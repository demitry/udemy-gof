using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace DesignPatterns
{
    //0. dotnet new gitignore

    class Program
    {
        #region SRP

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
                if(overwrite || !File.Exists(filename))
                {
                    File.WriteAllText(filename, journal.ToString());
                }
            }
        }

        #endregion

        #region OCP

        public enum Color
        {
            Red, Green, Blue
        }

        public enum Size
        {
            Small, Medium, Large, Huge
        }

        public class Product
        {
            public string Name;
            public Color Color;
            public Size Size;

            public Product(string name, Color color, Size size)
            {
                Name = name;
                Color = color;
                Size = size;
            }
        }

        public class ProductFilter
        {
            public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
            {
                foreach (var p in products)
                    if (p.Size == size)
                        yield return p;
            }

            public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
            {
                foreach (var p in products)
                    if (p.Color == color)
                        yield return p;
            }

            // etc. etc. and FilterBy Color and Size, etc. etc.
            // NO! 
        }

        public interface ISpecification<T>
        {
            bool IsSatisfied(T t);
        }
        
        public interface IFilter<T>
        {
            IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
        }

        public class ColorSpecification : ISpecification<Product>
        {
            Color color;

            public ColorSpecification(Color color)
            {
                this.color = color;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Color == color;
            }
        }

        public class SizeSpecification : ISpecification<Product>
        {
            Size size;

            public SizeSpecification(Size size)
            {
                this.size = size;
            }

            public bool IsSatisfied(Product t)
            {
                return t.Size == size;
            }
        }

        class BetterFilter : IFilter<Product>
        {
            public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
            {
                foreach (var i in items)
                    if (spec.IsSatisfied(i))
                        yield return i;
            }
        }

        public class AndSpecification<T> : ISpecification<T>
        {
            ISpecification<T> first, second;

            public AndSpecification(ISpecification<T> first, ISpecification<T> second)
            {
                this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
                this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
            }

            public bool IsSatisfied(T t)
            {
                return first.IsSatisfied(t) && second.IsSatisfied(t);
            }
        }

        #endregion

        static void Main(string[] args)
        {
            //SRP

            /*
            var journal = new Journal();
            journal.AddEntry("I cried today");
            journal.AddEntry("I ate a bug");
            WriteLine(journal);
            Console.WriteLine("Hello World!");

            var p = new Persistence();
            var filename = @"c:\temp\journal.txt";
            p.SaveToFile(journal, filename, true);
            */

            //OCP

            Product apple = new Product("Apple", Color.Green, Size.Small);
            Product tree = new Product("Tree", Color.Green, Size.Large);
            Product house = new Product("House", Color.Blue, Size.Huge);
            Product greenHouse = new Product("GreenHouse", Color.Green, Size.Huge);

            Product[] products = { apple, tree, house, greenHouse };

            var pf = new ProductFilter();
            WriteLine("Green products (old):");
            foreach (var product in pf.FilterByColor(products, Color.Green))
            {
                WriteLine($" - {product.Name} is green");
            }


            var bf = new BetterFilter();
            WriteLine("Green products (new):");
            foreach (var pr in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($" - {pr.Name} is green");
            }

            WriteLine("Green huge products (new):");
            foreach (var pr in bf.Filter(products, 
                new AndSpecification<Product>(
                    new ColorSpecification(Color.Green), 
                    new SizeSpecification(Size.Huge))
                ))
            {
                WriteLine($" - {pr.Name} is green and huge");
            }

        }
    }
}
