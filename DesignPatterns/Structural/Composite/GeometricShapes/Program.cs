using System.Text;

namespace GeometricShapes
{
    internal class Program
    {
        public class GraphicObject
        {
            public virtual string Name { get; set; } = "Group";
            public string Color = "White";

            private Lazy<List<GraphicObject>> children = new Lazy<List<GraphicObject>>();

            public List<GraphicObject> Children => children.Value;

            private void Print(StringBuilder stringBuilder, int depth)
            {
                stringBuilder
                    .Append(new string('*', depth))
                    .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color}")
                    .AppendLine(Name);

                foreach (var child in Children)
                {
                    child.Print(stringBuilder, depth + 1);
                }
            }

            public override string ToString()
            {
                var sb = new StringBuilder();
                Print(sb, 0);
                return sb.ToString();
            }
        }

        public class Circle : GraphicObject
        {
            public override string Name => "Circle";
        }

        public class Square : GraphicObject
        {
            public override string Name => "Square";
        }


        static void Main(string[] args)
        {
            var drawing = new GraphicObject { Name = "My Drawing", Color = "BlaColor" };
            drawing.Children.Add(new Circle { Color = "Red" });
            drawing.Children.Add(new Square { Color = "Yellow"});

            var group = new GraphicObject();
            group.Children.Add(new Circle { Color = "Blue" });
            group.Children.Add(new Square { Color = "Blue" });

            var innerGroup1 = new GraphicObject();
            innerGroup1.Children.Add(new Circle { Color = "Magenta" });
            innerGroup1.Children.Add(new Square { Color = "Magenta" });
            
            group.Children.Add(innerGroup1);

            drawing.Children.Add(new Circle { Color = "Red" });

            drawing.Children.Add(group);

            Console.WriteLine(drawing);
        }
    }
}