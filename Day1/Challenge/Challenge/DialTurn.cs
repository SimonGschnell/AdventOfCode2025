using System.Text.RegularExpressions;

namespace Challenge;

public partial record DialTurn
{
    public int Value { get; }
    public string Direction { get; }
    
    public DialTurn(string command)
    {
        Direction = GetDirectionFromCommand(command);
        Value = GetValueFromCommand(command);
    }

    private static int GetValueFromCommand(string directionString)
    {
        var stringValue = ValueRegex().Match(directionString).Value;
        var value = int.Parse(stringValue);
        return value;
    }

    private static string GetDirectionFromCommand(string directionString)
    {
        return DirectionRegex().Match(directionString).Value.ToUpper();
    }

    [GeneratedRegex(@"^[a-zA-Z]")]
    private static partial Regex DirectionRegex();
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex ValueRegex();
}