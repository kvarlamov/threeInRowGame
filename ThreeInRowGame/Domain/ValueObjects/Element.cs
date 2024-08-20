namespace ThreeInRowGame.Domain.ValueObjects;

public struct Element
{
    public string Value { get; private set; }

    public ConsoleColor Color { get; set; }

    public static Element Create(string value, ConsoleColor color = ConsoleColor.Green)
    {
        return new Element
        {
            Value = value,
            Color = color
        };
    }

    public static Element Clear()
    {
        return new Element {Value = " "};
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        return obj is Element element && !string.IsNullOrWhiteSpace(element.Value) && !string.IsNullOrWhiteSpace(Value) &&  element.Value == Value;
    }

    public bool Equals(Element other)
    {
        if (string.IsNullOrWhiteSpace(Value) || string.IsNullOrWhiteSpace(other.Value))
            return false;
        return Value == other.Value;
    }

    public bool Equals(string other)
    {
        return Value == other;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value;
    }

    public void SetValue(string val)
    {
        Value = val;
    }
}