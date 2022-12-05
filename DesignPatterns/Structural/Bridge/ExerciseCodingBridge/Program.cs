//Bridge Coding Exercise
//You are given an example of an inheritance hierarchy which results in Cartesian-product duplication.
//Please refactor this hierarchy, giving the base class Shape  a constructor that takes an interface IRenderer  defined as
//interface IRenderer
//{
//    string WhatToRenderAs { get; }
//}
//as well as VectorRenderer  and RasterRenderer  classes. Each implementer of the Shape  abstract class should have a constructor that takes an IRenderer  such that, subsequently, each constructed object's ToString()  operates correctly, for example,

//new Triangle(new RasterRenderer()).ToString() // returns "Drawing Triangle as pixels" 


namespace ExerciseCodingBridge
{
    public abstract class Shape
    {
        public string Name { get; set; } = string.Empty;
    }

    public class Triangle : Shape
    {
        public Triangle() => Name = "Triangle";
    }

    public class Square : Shape
    {
        public Square() => Name = "Square";
    }

    public class VectorSquare : Square
    {
        public override string ToString() => "Drawing {Name} as lines";
    }

    public class RasterSquare : Square
    {
        public override string ToString() => "Drawing {Name} as pixels";
    }

    // imagine VectorTriangle and RasterTriangle are here too

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}