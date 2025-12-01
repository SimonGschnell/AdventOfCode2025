using System.Text.RegularExpressions;

namespace Challenge;

public class Safe
{
    private const int SolutionDigit = 0;
    private const int StartingPosition = 50;
    private const int DialNumberRange = 100;
    private const string Left = "L";
    private const string Right = "R";
    public int DialPosition { get; private set; } = StartingPosition;
    public int SolutionCounter { get; private set; }

    public void TurnDialRight(int value)
    {
        DialPosition = (value + DialPosition) % DialNumberRange ;
        IncrementAtSolutionDigit();
    }
    
    public void TurnDialLeft(int value)
    {
        DialPosition = (DialPosition + DialNumberRange - value ) % DialNumberRange;
        IncrementAtSolutionDigit();
    }
    
    private void IncrementAtSolutionDigit()
    {
        if (DialPosition == SolutionDigit)
        {
            SolutionCounter++;
        }
    }

    public void PerformTurn(DialTurn command)
    {
        switch (command.Direction)
        {
            case Left:
                TurnDialLeft(command.Value);
                break;
            case Right:
                TurnDialRight(command.Value);
                break;
        }
    }

    
}

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