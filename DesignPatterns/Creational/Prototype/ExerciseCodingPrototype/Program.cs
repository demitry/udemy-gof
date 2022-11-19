//Given the definitions above, you are asked to implement Line.DeepCopy()  to perform a deep copy of the current Line  object.

using Coding.Exercise;

var line1 = new Line(new Point(2,2), new Point(20, 20));
var line2 = line1.DeepCopy();
Console.WriteLine(line1);
Console.WriteLine(line2);

namespace Coding.Exercise
{
    public interface IDeepCopyable<T>
    {
        T DeepCopy();
    }

    public class Point: IDeepCopyable<Point>
    {
        public int X, Y;

        public Point()
        {

        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point DeepCopy() => new Point(X, Y);

        public override string ToString() => $"({X}, {Y})";
    }

    public class Line : IDeepCopyable<Line>
    {
        public Point Start, End;

        public Line()
        {
            Start = new Point(0,0);
            End = new Point(0,0);
        }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Line DeepCopy() => new Line(Start.DeepCopy(), End.DeepCopy());

        public override string ToString() => $"Line {Start} - {End}";
    }
}