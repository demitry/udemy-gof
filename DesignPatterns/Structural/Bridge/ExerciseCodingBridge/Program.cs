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
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    public class RasterRenderer : IRenderer
    {
        string IRenderer.WhatToRenderAs => "pixels";
    }

    public class VectorRenderer : IRenderer
    {
        string IRenderer.WhatToRenderAs => "lines";
    }

    public abstract class Shape
    {
        protected IRenderer renderer;

        public string Name { get; set; } = string.Empty;

        public Shape(IRenderer renderer, string name)
        {
            this.renderer = renderer;
            Name = name;
        }

        public override string ToString() => $"Drawing {Name} as {renderer.WhatToRenderAs}";
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer renderer) : base(renderer, "")
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer renderer) : base(renderer, "")
        {
            Name = "Square";
        }
    }

    public class VectorSquare : Square
    {
        public VectorSquare() : base(new VectorRenderer()) {}
    }

    public class RasterSquare : Square
    {
        public RasterSquare() : base(new RasterRenderer()) {}
    }

    // imagine VectorTriangle and RasterTriangle are here too
    
    public class VectorTriangle : Triangle
    {
        public VectorTriangle() : base(new VectorRenderer()) { }
    }

    public class RasterTriangle : Triangle
    {
        public RasterTriangle() : base(new RasterRenderer()) { }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var triangle = new Triangle(new RasterRenderer()).ToString(); // returns "Drawing Triangle as pixels" 

            Console.WriteLine(triangle);

            var rs = new RasterSquare();
            Console.WriteLine(rs);

            var vs = new VectorSquare();
            Console.WriteLine(vs);

            var rt = new RasterTriangle();
            Console.WriteLine(rt);

            var vt = new VectorTriangle();
            Console.WriteLine(vt);
        }
    }
}