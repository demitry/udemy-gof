Console.WriteLine("Fix public constructor issue with inner factory class:");
var p = Point.Factory.NewCartesianPoint(3, 4);
Console.WriteLine("Point: " + p);

public class Point
{
    private double x, y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString() => $"{nameof(x)}:{x}, {nameof(y)}: {y}";
    
    //public static PointFactory Factory => new PointFactory();

    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
}

