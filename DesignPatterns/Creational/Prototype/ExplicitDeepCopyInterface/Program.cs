var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));
Console.WriteLine(john);

class Person
{
    public string[] Names;
    private Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
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

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }
}