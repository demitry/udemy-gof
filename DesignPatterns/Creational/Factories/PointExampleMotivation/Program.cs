﻿public class Point
{
    private double x, y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public Point(
        double a,
        double b, // names do not communicate intent
        CoordinateSystem cs = CoordinateSystem.Cartesian)
    {
        switch (cs)
        {
            case CoordinateSystem.Polar:
                x = a * Math.Cos(b);
                y = a * Math.Sin(b);
                break;
            default:
                x = a;
                y = b;
                break;
        }

        // steps to add a new system
        // 1. augment CoordinateSystem
        // 2. change ctor
    }

    // factory property

    public static Point Origin => new Point(0, 0);

    // singleton field
    public static Point Origin2 = new Point(0, 0);

    // factory method
    // understandable method name and parameters

    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
    }

    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    // make it lazy
    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }
    }

    public override string ToString() => $"{nameof(x)}:{x}, {nameof(y)}: {y}";
}

class PointFactory
{
    public static Point NewCartesianPoint(float x, float y)
    {
        return new Point(x, y); // needs to be public
    }
}

class Demo
{
    static void Main(string[] args)
    {
        var p11 = Point.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(p11);

        var p1 = new Point(2, 3, Point.CoordinateSystem.Cartesian);
        
        var origin = Point.Origin;

        var p2 = Point.Factory.NewCartesianPoint(1, 2);
    }
}
