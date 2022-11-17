// Sometimes a single builder isn't enough.
// Need Several builders for building several different aspect of particular object?
// Need nice API for building?

var personBuilder = new PersonBuilder();

//// get person from builder:
//...public static implicit operator Person(PersonBuilder pb) => pb.person;

/*var*/ 
Person person = personBuilder                 
    .Lives.At("123 Main St.")
          .In("London")
          .WithPostCode("SW12SDF")
    .Works.At("Fabrikam")
          .AsA("Engineer")
          .Earning(123000);

Console.WriteLine(person);

Person person2 = 
    personBuilder.Lives.At("56 Baker St.").In("London").WithPostCode("AAA12")
      .Works.At("Volvo").AsA("Engineer").Earning(18000);

Console.WriteLine(person);

Console.WriteLine("----------------------------------");

Console.WriteLine(person);
Console.WriteLine("NB! need new builder!");
Console.WriteLine(person2);

Console.WriteLine("----------------------------------");
Console.WriteLine("new builder - new person:");

Person person3 =
    new PersonBuilder()
        .Lives.At("321 Zelena St.").In("Lviv").WithPostCode("65A12")
        .Works.At("PLS").AsA("Engineer").Earning(8000);

Console.WriteLine(person3);

public class Person
{
    // address
    public string? StreetAddress, City, PostalCode;

    // empoyment
    public string? CompanyName, Position;
    public int AnnualIncome;

    public override string ToString() => 
        $"Address: {StreetAddress}, {City}, {PostalCode}; \n" +
        $"Works at {CompanyName} as an {Position} earning {AnnualIncome}";
}

public class PersonBuilder // facade
{
    // reference object ! Trouble with value type.
    protected Person person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    public static implicit operator Person(PersonBuilder pb) => pb.person;
    //Implisit conversion operator from PersonBuilder to Person, a bit tricky ...
}

public class PersonJobBuilder: PersonBuilder //wierd?
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int ammount)
    {
        person.AnnualIncome = ammount;
        return this;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostCode(string postCode)
    {
        person.PostalCode = postCode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        person.City = city;
        return this;
    }
}