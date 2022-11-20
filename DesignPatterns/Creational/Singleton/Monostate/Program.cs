var ceo = new CEO();
ceo.Name = "Adam Smith";
ceo.Age = 55;

var ceo2 = new CEO();
Console.WriteLine(ceo2);

// Static private data fields, public non-static properties -
// we allowed to call c-tor, "re-create" the object but use the shared static data.

public class CEO
{
    private static string name;
    private static int age;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public int Age
    {
        get => age;
        set => age = value;
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
    }
}
