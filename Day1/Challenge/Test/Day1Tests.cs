using System.Text.RegularExpressions;

namespace Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void SafeDialStartsAt50()
    {
        var safe = new Safe();
        Assert.That(safe.DialPosition, Is.EqualTo(50));
    }

    [Test]
    public void TurnSafeDialRightBy10()
    {
        Safe safe = new Safe();
        safe.TurnDialRight(10);
        Assert.That(safe.DialPosition, Is.EqualTo(60));
    }
    
    [Test]
    public void TurnSafeDialLeftBy15()
    {
        Safe safe = new Safe();
        safe.TurnDialLeft(15);
        Assert.That(safe.DialPosition, Is.EqualTo(35));
    }
    
    [Test]
    public void TurnSafeDialLeftBy55OnStart()
    {
        Safe safe = new Safe();
        safe.TurnDialLeft(55);
        Assert.That(safe.DialPosition, Is.EqualTo(95));
    }
    
    [Test]
    public void TurnSafeDialRIghtBy55OnStart()
    {
        Safe safe = new Safe();
        safe.TurnDialRight(55);
        Assert.That(safe.DialPosition, Is.EqualTo(5));
    }
    
    [Test]
    public void SafeDialPointingAt0IncreasesSolutionCounter()
    {
        Safe safe = new Safe();
        safe.TurnDialRight(50);
        Assert.That(safe.DialPosition, Is.EqualTo(0));
        Assert.That(safe.SolutionCounter, Is.EqualTo(1));
    }
    
    [Test]
    public void SafeDialPointingTwiceAt0()
    {
        Safe safe = new Safe();
        safe.TurnDialRight(50);
        safe.TurnDialRight(10);
        safe.TurnDialLeft(10);
        Assert.That(safe.DialPosition, Is.EqualTo(0));
        Assert.That(safe.SolutionCounter, Is.EqualTo(2));
    }
    
    [Test]
    public void ConvertsCommandToDialTurn()
    {
        Safe safe = new Safe();
        string command = "R11";
        safe.ExecuteCommand(command);
        Assert.That(safe.DialPosition, Is.EqualTo(61));
        Assert.That(safe.SolutionCounter, Is.EqualTo(0));
    }
    
    [Test]
    public void ReadsCommandsFromFileCorrectly()
    {
        Safe safe = new Safe();
        const string testInputFilePath = "TestInput.txt";
        foreach (var command in File.ReadLines(testInputFilePath))
        {
            safe.ExecuteCommand(command);
        }
        Assert.That(safe.DialPosition, Is.EqualTo(55));
        Assert.That(safe.SolutionCounter, Is.EqualTo(1));
    }
    
    [Test]
    public void CalculatesSolutionCorrectly()
    {
        Safe safe = new Safe();
        const string inputFilePath = "Day1Input.txt";
        foreach (var command in File.ReadLines(inputFilePath))
        {
            safe.ExecuteCommand(command);
        }
        Assert.That(safe.SolutionCounter, Is.EqualTo(980));
    }
}

public partial class Safe
{
    private const int SolutionDigit = 0;
    private const string Left = "L";
    private const string Right = "R";
    public int DialPosition { get; set; } = 50;
    public int SolutionCounter { get; set; }

    public void TurnDialRight(int value)
    {
        DialPosition = (value + DialPosition) % 100 ;
        IncrementAtSolutionDigit();
    }
    
    public void TurnDialLeft(int value)
    {
        DialPosition = (DialPosition + 100 - value ) % 100;
        IncrementAtSolutionDigit();
    }
    
    private void IncrementAtSolutionDigit()
    {
        if (DialPosition == SolutionDigit)
        {
            SolutionCounter++;
        }
    }

    public void ExecuteCommand(string command)
    {
        var direction = GetDirectionFromCommand(command);
        var value = GetValueFromCommand(command);
        switch (direction)
        {
            case Left:
                TurnDialLeft(value);
                break;
            case Right:
                TurnDialRight(value);
                break;
        }
    }

    private static int GetValueFromCommand(string command)
    {
        var stringValue = ValueRegex().Match(command).Value;
        var value = int.Parse(stringValue);
        return value;
    }

    private static string GetDirectionFromCommand(string command)
    {
        return DirectionRegex().Match(command).Value.ToUpper();
    }

    [GeneratedRegex(@"^[a-zA-Z]")]
    private static partial Regex DirectionRegex();
    
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex ValueRegex();
}