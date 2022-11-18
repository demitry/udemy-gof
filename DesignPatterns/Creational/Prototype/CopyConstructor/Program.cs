var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));
Console.WriteLine(john);

var jane = new Person(john);
jane.Address.HouseNumber = 321;
Console.WriteLine(jane);

// Copy Ctor - C++ approach, still not good for us...

class Person
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person(Person other)
    {
        Names = other.Names;
        Address = other.Address;
    }

    public override string ToString()
    {
        return $"{string.Join(" ", Names)}, {Address}";
    }
}

public class Address
{
    private string StreetName;
    public int HouseNumber;

    public override string ToString()
    {
        return $"{StreetName} {HouseNumber}";
    }

    public Address(Address other)
    {
        StreetName = other.StreetName;
        HouseNumber = other.HouseNumber;
    }

    public Address(string streetName, int houseBumber)
    {
        StreetName = streetName;
        HouseNumber = houseBumber;
    }
}