using ThreeInRowGame.Domain.ValueObjects;

namespace ThreeInRowGame.Domain;

public static class ElementFactory
{
    static readonly List<Element> Elements = new();

    public static void InitializeElements()
    {
        Elements.Add(Element.Create("A", ConsoleColor.Blue));
        Elements.Add(Element.Create("B", ConsoleColor.DarkMagenta));
        Elements.Add(Element.Create("C", ConsoleColor.Magenta));
        Elements.Add(Element.Create("D", ConsoleColor.Red));
        Elements.Add(Element.Create("E", ConsoleColor.Yellow));
    }

    public static Element GetNext()
    {
        Random rnd = new Random();
        var index = rnd.Next(0, Elements.Count);
        return Elements[index];
    }
}