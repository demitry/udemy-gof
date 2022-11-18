var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

Console.WriteLine(john);

//var jane = john;
var jane = (Person)john.Clone();

jane.Names[0] = "Jane";
Console.WriteLine(jane);
Console.WriteLine(john);

class Person : ICloneable //we don't know is it deep or shalow cloning? shalow cloning! It's bad!
{
    public string[] Names;
    private Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public object Clone()
    {
        return new Person(Names, (Address) Address.Clone()); //and clone, and clone...
    }

    public override string ToString()
    {
        return $"{string.Join(" ", Names)}, {Address}";
    }
}

public class Address: ICloneable
{
    private string StreetName;
    public int HouseNumber;
    
    public override string ToString()
    {
        return $"{StreetName} {HouseNumber}";
    }

    public object Clone()
    {
        return new Address(StreetName, HouseNumber);
    }

    public Address(string streetName, int houseBumber)
    {
        StreetName = streetName;
        HouseNumber = houseBumber;
    }
}