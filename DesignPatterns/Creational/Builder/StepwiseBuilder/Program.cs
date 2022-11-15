var car = CarBuilder.Create()
                    .OfType(CarType.Crossover)
                    .WithWheels(16)
                    .Build();

Console.WriteLine(car);

// If need specific order - firstly specify type, then other properties, then etc.
// + Also need some validation.

public enum CarType
{
    Sedan,
    Crossover
}

public class Car
{
    public CarType Type;
    public int WheelSize;
    public override string ToString() => $"{Type} with {WheelSize} wheels";
}

public interface ISpecifyCarType
{
    ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize
{
    IBuildCar WithWheels(int size);
}

public interface IBuildCar
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl : 
        ISpecifyCarType, 
        ISpecifyWheelSize, 
        IBuildCar
    {
        private Car car = new Car();

        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type; 
            return this;
        }

        IBuildCar ISpecifyWheelSize.WithWheels(int size)
        {
            switch (car.Type)
            {   
                case CarType.Sedan when size < 17 || size > 20:
                case CarType.Crossover when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheels for {car.Type}.");
            }
            car.WheelSize = size;
            return this;
        }

        public Car Build()
        {
            return car;
        }
    }

    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}

