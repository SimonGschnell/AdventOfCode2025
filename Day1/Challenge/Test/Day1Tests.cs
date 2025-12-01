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
}

public class Safe
{
    private const int SolutionDigit = 0;
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
}