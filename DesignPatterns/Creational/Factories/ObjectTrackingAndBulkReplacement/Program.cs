using System.Text;

// Udemy:
//https://www.udemy.com/course/design-patterns-csharp-dotnet/learn/lecture/28379706#overview
// C# WeakReference: 
//https://learn.microsoft.com/en-us/dotnet/standard/garbage-collection/weak-references

var factory = new TrackingThemeFactory();
var theme1 = factory.CreateTheme(false);
var theme2 = factory.CreateTheme(true);

Console.WriteLine(factory.Info);

Console.WriteLine("--------------------");

var replFactory = new ReplaceableTheamFactory();
var magicTheme = replFactory.CreateTheme(dark: true);

Console.WriteLine(magicTheme.Value.BackgroundColor);
replFactory.ReplaceTheme(dark: false);
Console.WriteLine(magicTheme.Value.BackgroundColor);

public interface ITheme
{ 
    string TextColor { get; }
    string BackgroundColor { get; }
}

class LightTheme : ITheme
{
    public string TextColor => "black";
    public string BackgroundColor => "white";
}

class DarkTheme : ITheme
{
    public string TextColor => "white";
    public string BackgroundColor => "dark gray";
}

public class TrackingThemeFactory
{
    private readonly List<WeakReference<ITheme>> themes = new();
    public ITheme CreateTheme(bool dark)
    {
        ITheme theme = dark ? new DarkTheme() : new LightTheme();
        themes.Add(new WeakReference<ITheme>(theme));
        return theme;
    }

    public string Info
    {
        get 
        {
            var sb = new StringBuilder();
            foreach (var reference in themes)
            {
                if(reference.TryGetTarget(out var theme))
                {
                    bool dark = theme is DarkTheme;
                    sb.Append(dark ? "Dark" : "Light").AppendLine(" theme");
                }
            }
            return sb.ToString();
        }
    }
}

public class ReplaceableTheamFactory
{
    private readonly List<WeakReference<Ref<ITheme>>> themes = new();

    private ITheme createThemeImpl(bool dark)
    {
        return dark ? new DarkTheme() : new LightTheme();
    }

    public Ref<ITheme> CreateTheme(bool dark)
    {
        var r = new Ref<ITheme>(createThemeImpl(dark));
        themes.Add(new(r));
        return r;
    }

    public void ReplaceTheme(bool dark)
    {
        foreach (var weakReference in themes)
        {
            if(weakReference.TryGetTarget(out var reference))
            {
                reference.Value = createThemeImpl(dark);
            }
        }
    }
}

public class Ref<T> where T: class
{
    public T Value;

    public Ref(T value)
    {
        Value = value;
    }
}