var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

var jane = john.DeepCopy();
jane.Address.HouseNumber = 1;

Console.WriteLine(john);
Console.WriteLine(jane);

//But we still stuck with this deep copy 20 modifications.

public interface IPrototype<T>
{
    T DeepCopy();
}

class Person: IPrototype<Person>
{
    public string[] Names;
    public Address Address;

    public Person(string[] names, Address address)
    {
        Names = names;
        Address = address;
    }

    public Person DeepCopy()
    {
        return new Person(Names, Address.DeepCopy());
    }

    public override string ToString()
    {
        return $"{string.Join(" ", Names)}, {Address}";
    }
}

public class Address: IPrototype<Address>
{
    public string StreetName;
    public int HouseNumber;

    public override string ToString()
    {
        return $"{StreetName} {HouseNumber}";
    }

    public Address DeepCopy()
    {
        return new Address(StreetName, HouseNumber);
    }

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }
}