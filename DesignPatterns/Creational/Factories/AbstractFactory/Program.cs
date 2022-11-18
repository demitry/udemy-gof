using static System.Console;

var machine = new HotDrinkMachine();
var americano = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Coffee, 200);
var tea = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 300);
     
public interface IHotDrink
{
    void Comsume();
}

internal class Tea : IHotDrink
{
    public void Comsume()
    {
        WriteLine("This tea is nice but I'd prefer it with milk.");
    }
}

internal class Coffee : IHotDrink
{
    public void Comsume()
    {
        WriteLine("This coffee is sensational");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    public enum AvailableDrink
    {
        Coffee, Tea
    }

    private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new();

    public HotDrinkMachine()
    {

        //Tricky, but it works.
        //foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
        //{
        //    var factory = (IHotDrinkFactory)Activator.CreateInstance(Type.GetType(Enum.GetName(typeof(AvailableDrink), drink) + "Factory"));
        //    factories.Add(drink, factory);
        //}

        factories.Add(AvailableDrink.Tea, new TeaFactory());
        factories.Add(AvailableDrink.Coffee, new CoffeeFactory());
    }

    public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    {
        return factories[drink].Prepare(amount);
    }
}