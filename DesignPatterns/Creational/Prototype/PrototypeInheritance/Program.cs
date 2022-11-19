var john = 
    new Employee(
        new [] { "John", "Doe" },
        new Address("Main Str", 123),
        12000);

var copy = john.DeepCopy();
copy.Names[0] = "Smith";
copy.Address.HouseNumber = 99;
copy.Sallary = 1231;

Console.WriteLine(john);
Console.WriteLine(copy);

public interface IDeepCopy<T>
{
    T DeepCopy();
}

//TODO: A lot of constructor repetition code

public class Address : IDeepCopy<Address>
{
    public string StreetName;
    public int HouseNumber;

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Address DeepCopy()
    {
        return new Address(StreetName, HouseNumber);
    }

    public override string ToString()
    {
        return $"{StreetName} {HouseNumber}";
    }
}

public class Person : IDeepCopy<Person>
{
    public string[] Names;
    public Address Address;

    public Person()
    {
        
    }

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person DeepCopy()
    {
        return new Person((string[])Names.Clone(), Address.DeepCopy());
    }

    public override string ToString()
    {
        return $"{string.Join(" ", Names)} {Address}";
    }
}

public class Employee : Person, IDeepCopy<Employee>
{
    public int Sallary;
    public Employee(string[] names, Address address, int sallary)
        : base(names, address)
    {
        Sallary = sallary;
    }

    public override string ToString()
    {
        return $"{string.Join(" ", Names)} {Address} Sallary: {Sallary}";
    }

    public new Employee DeepCopy()
    {
        return new Employee((string[])Names.Clone(), Address, Sallary);
    }
}