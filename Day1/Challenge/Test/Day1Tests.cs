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
}

public class Safe
{
    public int DialPosition { get; set; } = 50;

    public void TurnDialRight(int value)
    {
        DialPosition += value;
    }

    public void TurnDialLeft(int value)
    {
        DialPosition -= value;
    }
}