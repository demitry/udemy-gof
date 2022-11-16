using PatternLibrary;

var person = new PersonBuilder()
    .Called("Sarah")
    .WorksAs("Developer")
    .Build();

Console.WriteLine(person);

public class Person
{
    public string? Name, Position;
    public override string ToString() => $"{Name} works as {Position}";
}

//public sealed class PersonBuilder
//{
//    //Mutating functions
//    private readonly List<Func<Person, Person>> actions = new List<Func<Person, Person>>();
//    public PersonBuilder Called(string name) => Do(p => p.Name = name);
//    public PersonBuilder Do(Action<Person> action) => AddAction(action);
//    public Person Build() => actions.Aggregate(new Person(), (p, f) => f(p));
//    private PersonBuilder AddAction(Action<Person> action)
//    {
//        actions.Add(p =>
//        {
//            action(p);
//            return p;
//        });
//        return this;
//    }
//} //Let it be more generic -> FunctionalBuilder

public sealed class PersonBuilder: FunctionalBuilder<Person, PersonBuilder>
{
    public PersonBuilder Called(string name) => Do(p => p.Name = name);
}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs
        (this PersonBuilder builder, string position)
        => builder.Do(p => p.Position = position);
}