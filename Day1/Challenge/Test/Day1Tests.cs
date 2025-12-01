using Challenge;

namespace Test;

public class Tests
{
    
    [Test]
    public void SafeDialStartsAt50()
    {
        var safe = new Safe();
        Assert.That(safe.DialPosition, Is.EqualTo(50));
    }

    [Test]
    public void TurnSafeDialRightBy10()
    {
        var safe = new Safe();
        safe.TurnDialRight(10);
        Assert.That(safe.DialPosition, Is.EqualTo(60));
    }
    
    [Test]
    public void TurnSafeDialLeftBy15()
    {
        var safe = new Safe();
        safe.TurnDialLeft(15);
        Assert.That(safe.DialPosition, Is.EqualTo(35));
    }
    
    [Test]
    public void TurnSafeDialLeftBy55OnStart()
    {
        var safe = new Safe();
        safe.TurnDialLeft(55);
        Assert.That(safe.DialPosition, Is.EqualTo(95));
    }
    
    [Test]
    public void TurnSafeDialRIghtBy55OnStart()
    {
        var safe = new Safe();
        safe.TurnDialRight(55);
        Assert.That(safe.DialPosition, Is.EqualTo(5));
    }
    
    [Test]
    public void SafeDialPointingAt0IncreasesSolutionCounter()
    {
        var safe = new Safe();
        safe.TurnDialRight(50);
        Assert.That(safe.DialPosition, Is.EqualTo(0));
        Assert.That(safe.SolutionCounter, Is.EqualTo(1));
    }
    
    [Test]
    public void SafeDialPointingTwiceAt0()
    {
        var safe = new Safe();
        safe.TurnDialRight(50);
        safe.TurnDialRight(10);
        safe.TurnDialLeft(10);
        Assert.That(safe.DialPosition, Is.EqualTo(0));
        Assert.That(safe.SolutionCounter, Is.EqualTo(2));
    }
    
    [Test]
    public void ConvertsCommandToDialTurn()
    {
        var safe = new Safe();
        const string command = "R11";
        safe.PerformTurn(new DialTurn(command));
        Assert.That(safe.DialPosition, Is.EqualTo(61));
        Assert.That(safe.SolutionCounter, Is.EqualTo(0));
    }
    
    [Test]
    public void ReadsCommandsFromFileCorrectly()
    {
        var safe = new Safe();
        const string testInputFilePath = "TestInput.txt";
        foreach (var command in File.ReadLines(testInputFilePath))
        {
            safe.PerformTurn(new DialTurn(command));
        }
        Assert.That(safe.DialPosition, Is.EqualTo(55));
        Assert.That(safe.SolutionCounter, Is.EqualTo(1));
    }
    
    [Test]
    public void CalculatesSolutionCorrectly()
    {
        var safe = new Safe();
        const string inputFilePath = "Day1Input.txt";
        foreach (var command in File.ReadLines(inputFilePath))
        {
            safe.PerformTurn(new DialTurn(command));
        }
        Assert.That(safe.SolutionCounter, Is.EqualTo(980));
    }
}