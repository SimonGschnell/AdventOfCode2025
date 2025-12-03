using Logic;
using NUnit.Framework.Constraints;

namespace Test;

public class Tests
{

    [Test]
    public void BatteryIsCorrectlyCreated()
    {
        Battery battery = new Battery(8);
        Assert.That(battery.Joltage, Is.EqualTo(8));
    }
    
    [Test]
    public void BatteryJoltageCanNotBeGeaterThan9()
    {
        Assert.Throws<ArgumentException>(() =>
        {
            Battery battery = new Battery(10);
        });
        Assert.Throws<ArgumentException>(() =>
        {
            Battery battery = new Battery(99);
        });
        Assert.Throws<ArgumentException>(() =>
        {
            Battery battery = new Battery(111);
        });
    }
    
    [Test]
    public void BatteryBankIsCreatedCorrectly()
    {
        var batteryBank = new BatteryBank([
        new Battery(1),
        new Battery(2),
        new Battery(3),
        ]);
        Assert.That(batteryBank.Batteries, Has.Count.EqualTo(3));
    }
    
    [Test]
    public void JoltedProducedByBankIsCorrectlyCalculated()
    {
        var batteryBank = new BatteryBank([
            new Battery(8),
            new Battery(1),
            new Battery(9),
        ]);
        Assert.That(batteryBank.GetJoltage(), Is.EqualTo(89));
    }
    
    [Test]
    public void CorrectlyConvertsStringIntoBatteryBank()
    {
        var batteryBankString = "987654321111111";
        var batteryBank = BatteryBank.Create(batteryBankString);
        Assert.That(batteryBank.GetJoltage(), Is.EqualTo(98));
        Assert.That(batteryBank.Batteries, Has.Count.EqualTo(15));
    }

    [Test]
    public void CorrectlyCreatesBatteryCluster()
    {
        BatteryCluster cluster = new BatteryCluster([
            BatteryBank.Create("987654321111111"),
            BatteryBank.Create("987654321111111"),
            BatteryBank.Create("987654321111111")
        ]);
        Assert.That(cluster.GetJoltage(), Is.EqualTo(98+98+98));
        Assert.That(cluster.BatteryBanks, Has.Count.EqualTo(3));
    }
    
    [Test]
    public void CorrectlyCalculatesTheChallengeTestSets()
    {
        Assert.That(BatteryBank.Create("987654321111111").GetJoltage(), Is.EqualTo(98));
        Assert.That(BatteryBank.Create("811111111111119").GetJoltage(), Is.EqualTo(89));
        Assert.That(BatteryBank.Create("234234234234278").GetJoltage(), Is.EqualTo(78));
        Assert.That(BatteryBank.Create("818181911112111").GetJoltage(), Is.EqualTo(92));
    }
    
    [Test]
    public void CorrectlyCalculatesTheResultsOfDay3ChallengePart1()
    {
        BatteryCluster cluster = new BatteryCluster([]);
        foreach (var batteries in File.ReadAllLines("Day3Input.txt"))
        {
            cluster.BatteryBanks.Add(BatteryBank.Create(batteries));
        }

        Assert.That(cluster.GetJoltage(), Is.EqualTo(17158));
    }
}