using System.Xml.Serialization;

var john = new Person(new[] { "John", "Smith" }, new Address("London Road", 123));

var jane = john.DeepCopyXml();
jane.Names[0] = "Jane";

Console.WriteLine(john);
Console.WriteLine(jane);

Console.WriteLine("Seems, deep copy is not so deep...");

public static class ExtensionMethods
{
    //public static T DeepCopy<T>(this T self)
    //{

    //    //Error	SYSLIB0011	'BinaryFormatter.Serialize(Stream, object)' is obsolete: 'BinaryFormatter serialization is obsolete and should not be used. See https://aka.ms/binaryformatter for more information.'	

    //    /* Warning

    //    The BinaryFormatter type is dangerous and is not recommended for data processing. Applications should stop using BinaryFormatter as soon as possible, even if they believe the data they're processing to be trustworthy. BinaryFormatter is insecure and can't be made secure.
        
    //     */

    //    MemoryStream stream = new MemoryStream();
    //    BinaryFormatter formatter = new BinaryFormatter();
    //    formatter.Serialize(stream, self);
    //    stream.Seek(0, SeekOrigin.Begin);
    //    object copy = formatter.Deserialize(stream);
    //    stream.Close();
    //    return (T)copy;
    //}

    public static T DeepCopyXml<T>(this T self)
    {
        using (var ms = new MemoryStream())
        {
            XmlSerializer s = new XmlSerializer(typeof(T));
            s.Serialize(ms, self);
            ms.Position = 0;
            return (T)s.Deserialize(ms);
        }
    }
}


public class Person
{
    
    public string[] Names;
    private Address Address;

    public Person()
    {
        //System.InvalidOperationException: 'Person cannot be serialized because it does not have a parameterless constructor.'
    }

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
    private string StreetName = string.Empty;
    public int HouseNumber = 0;

    public Address()
    {

    }

    public override string ToString()
    {
        return $"{StreetName} {HouseNumber}";
    }

    public Address(string streetName, int houseBumber)
    {
        StreetName = streetName;
        HouseNumber = houseBumber;
    }
}