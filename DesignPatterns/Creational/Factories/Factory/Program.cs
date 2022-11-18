var p = PointFactory.NewCartesianPoint(3, 4);
Console.WriteLine("Point: " + p);

public class Point
{
    private double x, y;

    // have to open constructor to access from factory class
    public Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString() => $"{nameof(x)}:{x}, {nameof(y)}: {y}";
}

public static class PointFactory
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